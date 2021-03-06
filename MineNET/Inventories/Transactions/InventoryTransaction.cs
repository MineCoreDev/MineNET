﻿using System;
using System.Collections.Generic;
using System.Linq;
using MineNET.Blocks;
using MineNET.Entities.Players;
using MineNET.Inventories.Transactions.Action;
using MineNET.Items;

namespace MineNET.Inventories.Transactions
{
    public class InventoryTransaction
    {
        public bool HasExecuted { get; protected set; } = false;

        public Player Player { get; protected set; }

        public List<InventoryAction> Actions { get; protected set; } = new List<InventoryAction>();

        public List<Inventory> Inventories { get; protected set; } = new List<Inventory>();

        public InventoryTransaction(Player player, List<InventoryAction> actions)
        {
            this.Player = player;
            for (int i = 0; i < actions.Count; ++i)
            {
                this.AddAction(actions[i]);
            }
        }

        public virtual void AddAction(InventoryAction action)
        {
            if (this.Actions.Contains(action))
            {
                return;
            }
            action.AddInventory(this);
            this.Actions.Add(action);
        }

        public virtual void AddInventory(Inventory inventory)
        {
            this.Inventories.Add(inventory);
        }

        public virtual bool Execute()
        {
            if (this.HasExecuted || !this.CanExecute())
            {
                this.SendInventories();
                return false;
            }

            /*InventoryTransactionEventArgs inventoryTransactionEvent = new InventoryTransactionEventArgs(this);
            InventoryEvents.OnInventoryTransaction(inventoryTransactionEvent);
            if (inventoryTransactionEvent.IsCancel)
            {
                this.SendInventories();
                return false;
            }*/

            for (int i = 0; i < this.Actions.Count; ++i)
            {
                if (!this.Actions[i].OnPreExecute(this.Player))
                {
                    this.SendInventories();
                    return false;
                }
            }

            for (int i = 0; i < this.Actions.Count; ++i)
            {
                if (this.Actions[i].Execute(this.Player))
                {
                    this.Actions[i].OnExecuteSuccess(this.Player);
                }
                else
                {
                    this.Actions[i].OnExecuteFail(this.Player);
                }
            }
            this.HasExecuted = true;
            return true;
        }

        public virtual bool CanExecute()
        {
            this.SquashDuplicateSlotChanges();
            return this.MatchItems() && this.Actions.Count > 0;
        }

        public bool SquashDuplicateSlotChanges()
        {
            Dictionary<int, List<SlotChangeAction>> slotChanges = new Dictionary<int, List<SlotChangeAction>>();
            for (int i = 0; i < this.Actions.Count; ++i)
            {
                if (this.Actions[i] is SlotChangeAction)
                {
                    SlotChangeAction action = (SlotChangeAction) this.Actions[i];
                    int hash = action.Inventory.GetHashCode() | action.InventorySlot;
                    List<SlotChangeAction> list;
                    if (slotChanges.ContainsKey(hash))
                    {
                        list = slotChanges[hash];
                    }
                    else
                    {
                        list = new List<SlotChangeAction>();
                    }
                    list.Add(action);
                    slotChanges[hash] = list;
                }
            }

            KeyValuePair<int, List<SlotChangeAction>>[] entries = slotChanges.ToArray();
            for (int c = 0; c < entries.Length; ++c)
            {
                int hash = entries[c].Key;
                List<SlotChangeAction> list = entries[c].Value;

                if (list.Count == 1)
                {
                    slotChanges.Remove(hash);
                    continue;
                }

                List<SlotChangeAction> originalList = new List<SlotChangeAction>(list);

                SlotChangeAction originalAction = null;
                Item lastTargetItem = null;

                for (int i = 0; i < list.Count; ++i)
                {
                    SlotChangeAction action = list[i];
                    if (action.IsValid(this.Player))
                    {
                        originalAction = action;
                        lastTargetItem = action.TargetItem;
                        list.RemoveAt(i);
                        break;
                    }
                }
                if (originalAction == null)
                {
                    return false;
                }

                int sortedThisLoop;

                do
                {
                    sortedThisLoop = 0;
                    for (int i = 0; i < list.Count; ++i)
                    {
                        SlotChangeAction action = list[i];

                        Item actionSource = action.SourceItem;
                        if (actionSource == lastTargetItem)
                        {
                            lastTargetItem = action.TargetItem;
                            list.RemoveAt(i);
                            sortedThisLoop++;
                        }
                        else if (actionSource.Equals(lastTargetItem, true, false, true, true))
                        {
                            lastTargetItem.Count -= actionSource.Count;
                            list.RemoveAt(i);
                            if (lastTargetItem.Count == 0)
                            {
                                sortedThisLoop++;
                            }
                        }
                    }
                } while (sortedThisLoop > 0);

                if (list.Count > 0)
                {
                    return false;
                }

                for (int i = 0; i < originalList.Count; ++i)
                {
                    this.Actions.Remove(originalList[i]);
                }
                this.AddAction(new SlotChangeAction(originalAction.Inventory, originalAction.InventorySlot, originalAction.SourceItem, lastTargetItem));
            }
            return true;
        }

        public bool MatchItems()
        {
            List<Item> haveItems = new List<Item>();
            List<Item> needItems = new List<Item>();
            for (int i = 0; i < this.Actions.Count; ++i)
            {
                InventoryAction action = this.Actions[i];
                if (action.TargetItem.ID != BlockIDs.AIR && action.TargetItem.Count > 0)
                {
                    needItems.Add(action.TargetItem.Clone());
                }
                if (!action.IsValid(this.Player))
                {
                    return false;
                }
                if (action.SourceItem.ID != BlockIDs.AIR && action.SourceItem.Count > 0)
                {
                    haveItems.Add(action.SourceItem.Clone());
                }
            }

            List<Item> have = new List<Item>();
            for (int i = 0; i < haveItems.Count; ++i)
            {
                have.Add(haveItems[i]);
            }
            List<Item> need = new List<Item>();
            for (int i = 0; i < needItems.Count; ++i)
            {
                need.Add(needItems[i]);
            }
            for (int i = 0; i < have.Count; ++i)
            {
                for (int j = 0; j < need.Count; ++j)
                {
                    Item haveItem = have[i];
                    Item needItem = need[j];
                    if (haveItem.Equals(needItem, true, false))
                    {
                        int amount = Math.Min(haveItem.Count, needItem.Count);
                        haveItem.Count -= amount;
                        needItem.Count -= amount;
                        if (haveItem.Count == 0)
                        {
                            haveItems.Remove(haveItem);
                        }
                        if (needItem.Count == 0)
                        {
                            needItems.Remove(needItem);
                        }
                    }
                }
            }

            return haveItems.Count == 0 && needItems.Count == 0;
        }

        public virtual void SendInventories()
        {
            for (int i = 0; i < this.Inventories.Count; ++i)
            {
                this.Inventories[i].SendContents(this.Player);
            }
        }
    }
}

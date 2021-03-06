﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private static InventoryController instance;
    public static InventoryController Instance { get { return instance; } }

    // Item movement variables
    private bool currentlyMovingItem = false; // Whether or not we are in the process of moving an item
    public ItemSlot cursorIcon; // Visual for moving item

    // Graphic Raycaster code from https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public Inventory inventory;

    public delegate void EarningsChanged();
    public EarningsChanged OnEarningsChanged;

    private bool clicked = false;
    private Vector2 mousePosition;

    public int money = 1000;

    public CharacterStats statSheet;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        inventory.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this == null) return;

        if (StageManager.Instance == null)
            inventory.GetComponent<RectTransform>().localScale = Vector3.zero;
        else
            inventory.GetComponent<RectTransform>().localScale = new Vector3(0.2f, 0.2f, 1);
    }

    public void OnMouseMove(InputValue delta)
    {
        mousePosition = delta.Get<Vector2>();
    }

    public void SetMoneyAmount(int amount)
    {
        money = amount;
        if (OnEarningsChanged == null) return;
        OnEarningsChanged();
    }

    public void OnHotbar1(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[0].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar2(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[1].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar3(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[2].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar4(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[3].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar5(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[4].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar6(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[5].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar7(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[6].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar8(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[7].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar9(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[8].UseItem(StageManager.Instance.playerCharacter);
    }

    public void OnHotbar0(InputValue button)
    {
        if (StageManager.Instance == null) return;

        inventory.itemSlots[9].UseItem(StageManager.Instance.playerCharacter);
    }

    public void AddToInventory(Item item, int amount = 1)
    {
        foreach (ItemSlot itemSlot in inventory.itemSlots)
        {
            if (itemSlot.AddItems(item, amount))
            {
                GameManager.Instance.SaveGame();
                return;
            }
        }
    }

    // Allows player to pickup one item at a time.
    public void PickUpOne(ItemSlot itemSlot)
    {
        if (itemSlot.HasItem() &&
            (!cursorIcon.HasItem() ||
            itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
        {
            currentlyMovingItem = true;

            // Increase amount being moved by 1
            cursorIcon.AddItems(itemSlot.ItemInSlot, 1);

            // Decrease items in slot by 1
            itemSlot.TryRemoveItems(1);

            if (itemSlot.GetComponent<ResultSlot>())
            {
                itemSlot.gameObject.GetComponent<ResultSlot>().ConsumeIngredients();
            }
        }
        // Has the option to swap held item and slot item, but not part of requirement
        //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
        //{
        //    SwapHeldItem(itemSlot);
        //}
    }

    // Allows the player to pickup the entire stack of items.
    public void PickUpAll(ItemSlot itemSlot)
    {
        if (itemSlot.HasItem() &&
            (!cursorIcon.HasItem() ||
            itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
        {
            currentlyMovingItem = true;

            // Amount being moved is equal to the number of items in the slot
            cursorIcon.AddItems(itemSlot.ItemInSlot, itemSlot.ItemCount);

            // Remove all items, which is the amount of items in the slot
            itemSlot.TryRemoveItems(itemSlot.ItemCount);

            if (itemSlot.GetComponent<ResultSlot>())
            {
                itemSlot.gameObject.GetComponent<ResultSlot>().ConsumeIngredients();
            }
        }
        // Has the option to swap held item and slot item, but not part of requirement
        //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
        //{
        //    SwapHeldItem(itemSlot);
        //}
    }

    // Allows player to drop one item into a slot.
    public void DropOne(ItemSlot itemSlot)
    {
        if (itemSlot.canSetSlot)
        {
            if (cursorIcon.HasItem() &&
                (!itemSlot.HasItem() ||
                itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
            {
                if (cursorIcon.TryRemoveItems(1) > 0)
                {
                    itemSlot.AddItems(cursorIcon.ItemInSlot, 1);
                }
                if (cursorIcon.ItemCount <= 0)
                {
                    currentlyMovingItem = false;
                }
            }
            // Has the option to swap held item and slot item, but not part of requirement
            //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
            //{
            //    SwapHeldItem(itemSlot);
            //}
        }
    }

    // Allows a player to drop all currently held items into a slot.
    public void DropAll(ItemSlot itemSlot)
    {
        if (itemSlot.canSetSlot)
        {
            if (cursorIcon.HasItem() &&
                (!itemSlot.HasItem() ||
                itemSlot.ItemInSlot.ItemID == cursorIcon.ItemInSlot.ItemID))
            {
                if (cursorIcon.ItemCount > 0)
                {
                    itemSlot.AddItems(cursorIcon.ItemInSlot, cursorIcon.ItemCount);
                    cursorIcon.TryRemoveItems(cursorIcon.ItemCount);
                    currentlyMovingItem = false;
                }
            }
            // Has the option to swap held item and slot item, but not part of requirement
            //else if (itemSlot.HasItem() && cursorIcon.HasItem() && itemSlot.canSetSlot)
            //{
            //    SwapHeldItem(itemSlot);
            //}
        }
    }

    // Swap currently held item with the one in the slot.  Unused.
    public void SwapHeldItem(ItemSlot itemSlot)
    {
        // Swap the item in the slot with the item being transferred

        Item itemTemp = itemSlot.ItemInSlot;
        int countTemp = itemSlot.ItemCount;

        itemSlot.SetContents(cursorIcon.ItemInSlot, cursorIcon.ItemCount);
        // Swap the cursor icon
        cursorIcon.SetContents(itemTemp, countTemp);

        if (cursorIcon.HasItem())
        {
            currentlyMovingItem = true;
        }
    }

    // Bare minimum
    public void MoveItem(ItemSlot itemSlot)
    {
        if (currentlyMovingItem)
        {
            DropOne(itemSlot);
        }
        else if (!currentlyMovingItem)
        {
            // If picking up a crafted item, pickup all output
            if (itemSlot.GetComponent<ResultSlot>())
            {
                PickUpAll(itemSlot);
            }
            else
            {
                PickUpOne(itemSlot);
            }
        }
    }

    public void Cancel()
    {
        cursorIcon.TryRemoveItems(cursorIcon.ItemCount);
        currentlyMovingItem = false;
    }
}
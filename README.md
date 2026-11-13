# Unstacker

**Current release: 0.1.1 — for Kingdom Inc game version 1.0.14.**

Reduce a building's stack one level at a time with **Ctrl + Shift + left-click**.

## How to use

- Hold **Ctrl + Shift** and left-click the body of a stacked building.
- Each click removes one level and refunds its full current building price, using the kingdom's building count before removal.
- Unstacking stops at one building. The remaining building keeps its upgrades, recipe, position, stored items, and pipe connections.
- **Ctrl + left-click** continues to add stack levels as usual.

Unstacking is unavailable while dragging or using the deconstruction tool. Clicking a button or a connection port does not unstack the building.

## Requirements

- Kingdom Inc with BepInEx 5 installed.
- [Foundation](https://www.nexusmods.com/kingdominc/mods/18) version **0.14.0 or later**.

## Installation

Close the game, then place `Unstacker.dll` and `mod.json` inside `BepInEx/plugins/Unstacker/`. Keep Foundation installed separately.

## Updating and removal

Close the game before replacing or removing the `Unstacker` plugin folder. Stack changes use the game's normal save data and remain after removing Unstacker.

## Compatibility

| Class.Method | Hook Type | Impact |
|---|---|---|
| WorldFlowElement.OnPointerClick | Foundation hook (prefix) | Handles Ctrl+Shift+left-click as unstacking; other clicks follow normal game behavior. May conflict with mods that replace building clicks. |
| WorldFlowElement._canStackElement | Foundation hook (postfix) | Hides the add-stack hint and purchase cost while Ctrl+Shift is held. |

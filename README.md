# Item Weight Customizer

## Summary

Allows to customize weights of different items by Record type and Custom categories.

---

## Configuration File

The patcher will use a `config.json` file in order to determine what weights need to be set for different types of items/records.

- weightSettings: Define custom weight values depending on record type. If set to -1 they will be ignored.
    - books: Weight to be set for Books.
    - ingredients: Weight to be set for Ingredients.
    - scrolls: Weight to set for Scrolls.
    - soulgems: Weight to set for Soul Gems.
    - armors: Weight to set for Armours.
    - weapons: Weight to set for Weapons.
    - foods: Weight to set for Foods. (Having keyword VendorItemFood)
    - potions: Weight to set for Potions.
    - ingots: Weight to set for Ingot/Ores (Having keyword VendorItemOreIngot)
    - gems: Weight to set for Gems (Having keyword VendorItemGem)
    - animalParts: Weight to set for Animal Parts (Having keyword VendorItemAnimalPart)
    - animalHides: Weight to set for Animal Hides (Having keyword VendorItemAnimalHide)
    - clutter: Weight to set for Clutter items (Having keyword VendorItemClutter)
    - miscItems: Weight to set for all other Miscellaneous Items.
- categories: Custom user-defined categories that override the above record-based settings.
    - name: Each category needs to have a unique name (can be anything to keep track of, will be printed in logs)
    - types: All the record types (from above) this category will work against.
    - editorIds: List of Editor Ids. Record MUST have at least one Editor Id to be part of this category.
    - weight: The weight value that will be assigned for anything that matches this category.

---

## Change Log
> Released an early version that supports setting Custom Book Weight.
> Added Armours and Weapons weight configurability.
> Added custom category support based on Editor Ids for all the existing record types.
> Added Foods and Potions weight configurability.
> Added several variation of Miscellaneous Items.

# Item Weight Customizer

## Summary

Allows to customize weights of different items by Record type and Custom categories.
Also incorporates True Realistic Item Weights SSEEdit patcher (https://www.nexusmods.com/skyrimspecialedition/mods/11496?), to set multipliers for the weights of items according to categories based of TRIW. (added by profaneblood)

Formula: new weight = weight * weight settings

---

## Configuration File

The weights can be configured on the Settings tab. The available categories are the following:

- weightSettings: Define custom weight values depending on record type. If set to -1 they will be ignored.
	- books: Weight to be set for Books.
	- notes: Weight to be set for notes, journals and recipes.
	- ingredients: Weight to be set for Ingredients.
	- scrolls: Weight to set for Scrolls.
	- soulgems: Weight to set for Soul Gems.
	- armors: Weight to set for Armours.
		- light Cuirasses: Weight to set for light armor cuirasses.
		- light Others: Weight to set for any other piece of light armor.
		- clothes body: Weight to set for main (chest) articles of clothing.
		- clothes feet: Weight to set for feet articles of clothing.
		- clothing circlets: Weight to set for circlets.
		- jewels: Weight to set for jewelry.
	- weapons: Weight to set for Weapons.
	- foods: Weight to set for Foods. (Having keyword VendorItemFood)
		- wines brandys: Weight set for food items containing "wine" or "brandy" on their EditorID.
		- ales meads: Weight set for food items containing "ale" or "mead" on their EditorID.
		- breads flours: Weight set for food items containing "bread" or "flour" on their EditorID.
		- vegetables: Weight set for food items containing the strings "veg" and "soup" on their EditorID.
		- soups: Weight to set for food items containing "soup" on their EditorID.
		- meats: Weight to set for food items containing "meat" on their EditorID.
		- sea foods: Weight to set for food items containing "sea" on their EditorID.
		- drinks: Weight to set for food items containing "drink" on their EditorID.
	- potions: Weight to set for Potions.
	- ingots: Weight to set for Ingot/Ores (Having keyword VendorItemOreIngot)
	- gems: Weight to set for Gems (Having keyword VendorItemGem)
	- tools: Weight to set for Tools (Having keyword VendorItemTool)
	- tents: Weight to set for Tents (Having keyword isCampfireTentItem Campfire support)
	- silverware: Weight to set for  (Having keyword GiftUniversallyValuable)
	- animal parts: Weight to set for Animal Parts (Having keyword VendorItemAnimalPart)
		- animal pelts: Weight to set for items containing "pelt" on their EditorID.
		- animalHides: Weight to set for Animal Hides (Having keyword VendorItemAnimalHide)
	- clutter: Weight to set for Clutter items (Having keyword VendorItemClutter)
	- miscItems: Weight to set for all other Miscellaneous Items.
	- empty bottles: Weight to set for empty bottles.

- categories: Custom user-defined categories that override the above record-based settings.
    	- name: Each category needs to have a unique name (can be anything to keep track of, will be printed in logs)
    	- types: All the record types (from above) this category will work against.
    	- editorIds: List of Editor Ids. Record MUST have at least one Editor Id to be part of this category.
    	- weight: The weight value that will be assigned for anything that matches this category.

---

## Change Log
- Released an early version that supports setting Custom Book Weight.
- Added Armours and Weapons weight configurability.
- Added custom category support based on Editor Ids for all the existing record types.
- Added Foods and Potions weight configurability.
- Added several variation of Miscellaneous Items.
- More categories to classify.
- Settings are accessible through the settings tab of the Synthesis Program.
- Rather than a fixed number, the weight to set is calculated by multiplying the original weight by the number set for the category at the "Weight Settings" section of 	the settings tab.

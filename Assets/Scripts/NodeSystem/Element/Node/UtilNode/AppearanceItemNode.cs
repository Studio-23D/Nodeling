﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
	public abstract class AppearanceItemNode : DropdownNode <AppearanceItem>
	{
		protected abstract string ResourcePath {
			get;
		}

        [InputPropperty]
        public Color kleur = Color.white;

		[OutputPropperty]
		public AppearanceItem uitvoer;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			foreach (AppearanceItem item in Resources.LoadAll<AppearanceItem>(ResourcePath)){
				DropdownElement<AppearanceItem> element = new DropdownElement<AppearanceItem>
				{
					visual = item.Icon.texture,
					value = item
				};

				dropdownElements.Add(element);
			}

            base.Init(position, eventHandeler);

			CalculateChange();

			originalSize = new Vector2(MainSize.x, MainSize.y);
		}

		public override void CalculateChange()
		{
			this.uitvoer = chosenValue;

			this.uitvoer.SetColor(kleur);

			base.CalculateChange();
		}

		public override void Draw()
		{
			base.Draw();

			GUI.Label(new Rect(0, nodeAreas[2].y + 20, nodeAreas[2].width, nodeAreas[2].width), uitvoer.Icon.texture);
			GUI.Box(nodeAreas[3], "", styleBottomArea);
			GUI.EndGroup();
		}
	}
}


﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Transformations;
using osu.Framework.Graphics.UserInterface;

namespace osu.Game.Graphics.UserInterface
{
    class Nub : Container, IStateful<CheckBoxState>
    {
        public const float COLLAPSED_SIZE = 20;
        public const float EXPANDED_SIZE = 40;

        private Box fill;

        const float border_width = 3;
        private Color4 glowingColour, idleColour;

        public Nub()
        {
            Size = new Vector2(COLLAPSED_SIZE, 12);

            Masking = true;

            CornerRadius = Height / 2;
            Masking = true;
            BorderColour = Color4.White;
            BorderThickness = border_width;

            Children = new[]
            {
                fill = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.01f, //todo: remove once we figure why containers aren't drawing at all times
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = idleColour = colours.Pink;
            glowingColour = colours.PinkLighter;

            EdgeEffect = new EdgeEffect
            {
                Colour = colours.PinkDarker,
                Type = EdgeEffectType.Glow,
                Radius = 10,
                Roundness = 8,
            };

            FadeGlowTo(0);
        }

        public bool Glowing
        {
            set
            {
                if (value)
                {
                    FadeColour(glowingColour, 500, EasingTypes.OutQuint);
                    FadeGlowTo(1, 500, EasingTypes.OutQuint);
                }
                else
                {
                    FadeGlowTo(0, 500);
                    FadeColour(idleColour, 500);
                }
            }
        }

        public bool Expanded
        {
            set
            {
                ResizeTo(new Vector2(value ? EXPANDED_SIZE : COLLAPSED_SIZE, 12), 500, EasingTypes.OutQuint);
            }
        }

        private CheckBoxState state;

        public CheckBoxState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;

                switch (state)
                {
                    case CheckBoxState.Checked:
                        fill.FadeIn(200, EasingTypes.OutQuint);
                        break;
                    case CheckBoxState.Unchecked:
                        fill.FadeTo(0.01f, 200, EasingTypes.OutQuint); //todo: remove once we figure why containers aren't drawing at all times
                        break;
                }
            }
        }
    }
}

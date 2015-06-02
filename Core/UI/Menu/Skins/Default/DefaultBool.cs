﻿namespace LeagueSharp.SDK.Core.UI.Skins.Default
{
    using LeagueSharp.SDK.Core.Enumerations;
    using LeagueSharp.SDK.Core.Math;
    using LeagueSharp.SDK.Core.UI.Values;

    using SharpDX;
    using SharpDX.Direct3D9;

    /// <summary>
    /// A default implementation of IDrawableBool
    /// </summary>
    public class DefaultBool : DefaultComponent, IDrawableBool
    {
        /// <summary>
        /// Draws a MenuBool
        /// </summary>
        /// <param name="component">MenuBool</param>
        public void Draw(MenuBool component)
        {
            var centerY =
                (int)
                GetContainerRectangle(component.Container)
                    .GetCenteredText(null, component.Container.DisplayName, CenteredFlags.VerticalCenter)
                    .Y;

            DefaultSettings.Font.DrawText(
                MenuManager.Instance.Sprite,
                component.Container.DisplayName,
                (int)(component.Container.Position.X + DefaultSettings.ContainerTextOffset),
                centerY,
                DefaultSettings.TextColor);

            var line = new Line(Drawing.Direct3DDevice)
                           { Antialias = false, GLLines = true, Width = DefaultSettings.ContainerHeight };
            line.Begin();
            line.Draw(
                new[]
                    {
                        new Vector2(
                            (component.Container.Position.X + component.Container.MenuWidth
                             - DefaultSettings.ContainerHeight) + DefaultSettings.ContainerHeight / 2f,
                            component.Container.Position.Y + 1),
                        new Vector2(
                            (component.Container.Position.X + component.Container.MenuWidth
                             - DefaultSettings.ContainerHeight) + DefaultSettings.ContainerHeight / 2f,
                            component.Container.Position.Y + DefaultSettings.ContainerHeight)
                    },
                component.Value ? new ColorBGRA(0, 100, 0, 255) : new ColorBGRA(255, 0, 0, 255));
            line.End();
            line.Dispose();

            var centerX =
                (int)
                new Rectangle(
                    (int)
                    (component.Container.Position.X + component.Container.MenuWidth - DefaultSettings.ContainerHeight),
                    (int)component.Container.Position.Y,
                    DefaultSettings.ContainerHeight,
                    DefaultSettings.ContainerHeight).GetCenteredText(
                        null,
                        component.Value ? "ON" : "OFF",
                        CenteredFlags.HorizontalCenter).X;
            DefaultSettings.Font.DrawText(
                MenuManager.Instance.Sprite,
                component.Value ? "ON" : "OFF",
                centerX,
                centerY,
                DefaultSettings.TextColor);
        }

        /// <summary>
        /// Returns the Rectangle that defines the on/off button
        /// </summary>
        /// <param name="component">MenuBool</param>
        /// <returns>Rectangle</returns>
        public Rectangle ButtonBoundaries(MenuBool component)
        {
            return new Rectangle(
                (int)(component.Container.Position.X + component.Container.MenuWidth - DefaultSettings.ContainerHeight),
                (int)component.Container.Position.Y,
                DefaultSettings.ContainerHeight,
                DefaultSettings.ContainerHeight);
        }
    }
}
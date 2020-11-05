namespace mprExteriorPlanDimensions
{
    using System;
    using System.Collections.Generic;
    using ModPlusAPI.Abstractions;
    using ModPlusAPI.Enums;

    /// <inheritdoc/>
    public class ModPlusConnector : IModPlusPlugin
    {
        /// <inheritdoc/>
        public SupportedProduct SupportedProduct => SupportedProduct.Revit;

        /// <inheritdoc/>
        public string Name => "mprExteriorPlanDimensions";

#if R2017
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2017";
#elif R2018
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2018";
#elif R2019
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2019";
#elif R2020
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2020";
#elif R2021
        /// <inheritdoc/>
        public string AvailProductExternalVersion => "2021";
#endif

        /// <inheritdoc/>
        public string FullClassName => "mprExteriorPlanDimensions.Commands.ExteriorPlanDimensionsCommand";

        /// <inheritdoc/>
        public string AppFullClassName => string.Empty;

        /// <inheritdoc/>
        public Guid AddInId => Guid.Empty;

        /// <inheritdoc/>
        public string LName => "Наружные размеры на плане";

        /// <inheritdoc/>
        public string Description => "Простановка наружных размеров на плане этажа";

        /// <inheritdoc/>
        public string Author => "Пекшев Александр aka Modis";

        /// <inheritdoc/>
        public string Price => "0";

        /// <inheritdoc/>
        public bool CanAddToRibbon => true;

        /// <inheritdoc/>
        public string FullDescription => "Плагин позволяет в один клик проставить наружные размеры на плане этажа. Плагин обрабатывает только горизонтальные и вертикальные стены и оси. Простановка наружных размеров происходит согласно текущей рабочей конфигурации, в которой указываются стороны простановки размеров и настройки размерных цепочек";

        /// <inheritdoc/>
        public string ToolTipHelpImage => string.Empty;

        /// <inheritdoc/>
        public List<string> SubPluginsNames => new List<string> { "mprExteriorPlanDimensionsSettings" };

        /// <inheritdoc/>
        public List<string> SubPluginsLNames => new List<string> { "Наружные размеры. Настройки" };

        /// <inheritdoc/>
        public List<string> SubDescriptions => new List<string> { "Настройки рабочих конфигураций для наружных размеров на плане этажа" };

        /// <inheritdoc/>
        public List<string> SubFullDescriptions => new List<string> { string.Empty };

        /// <inheritdoc/>
        public List<string> SubHelpImages => new List<string> { string.Empty };

        /// <inheritdoc/>
        public List<string> SubClassNames => new List<string> { "mprExteriorPlanDimensions.Commands.ExteriorPlanDimensionsSettingsCommand" };
    }
}
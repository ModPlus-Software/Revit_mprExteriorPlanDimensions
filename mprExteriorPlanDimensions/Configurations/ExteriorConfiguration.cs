﻿namespace mprExteriorPlanDimensions.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Xml.Linq;
    using ModPlusAPI;

    /// <inheritdoc />
    /// <summary>Конфигурация образмеривания наружных стен </summary>
    public class ExteriorConfiguration : BaseConfiguration
    {
        private const string LangItem = "mprExteriorPlanDimensions";

        #region Constructor

        public ExteriorConfiguration()
        {
            Id = Guid.NewGuid();
            Chains = new CustomNotifyCollection<ExteriorDimensionChain>();
            Chains.CollectionChanged += Chains_CollectionChanged;
            Chains.Add(new ExteriorDimensionChain());
        }

        public ExteriorConfiguration(Guid id)
        {
            Id = id;
            Chains = new CustomNotifyCollection<ExteriorDimensionChain>();
            Chains.CollectionChanged += Chains_CollectionChanged;
        }

        #endregion

        #region Properties

        /// <summary>Коллекция конфигураций размерных цепочек</summary>
        public CustomNotifyCollection<ExteriorDimensionChain> Chains { get; set; }

        #region Directions

        private bool _leftDimensions;

        /// <summary>Размеры слева от плана</summary>
        public bool LeftDimensions
        {
            get => _leftDimensions;
            set
            {
                _leftDimensions = value;
                OnPropertyChanged(nameof(LeftDimensions));
            }
        }

        private bool _rightDimensions;

        /// <summary>Размеры справа от плана</summary>
        public bool RightDimensions
        {
            get => _rightDimensions;
            set
            {
                _rightDimensions = value;
                OnPropertyChanged(nameof(RightDimensions));
            }
        }

        private bool _topDimensions;

        /// <summary>Размеры сверху от плана</summary>
        public bool TopDimensions
        {
            get => _topDimensions;
            set
            {
                _topDimensions = value;
                OnPropertyChanged(nameof(TopDimensions));
            }
        }

        private bool _bottomDimensions;

        /// <summary>Размеры сверху от плана</summary>
        public bool BottomDimensions
        {
            get => _bottomDimensions;
            set
            {
                _bottomDimensions = value;
                OnPropertyChanged(nameof(BottomDimensions));
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>Получение настроек образмеривания наружных стен из XElement</summary>
        /// <param name="xElement"></param>
        /// <returns></returns>
        public static ExteriorConfiguration GetExteriorConfigurationFromXElement(XElement xElement)
        {
            var idAttr = xElement.Attribute(nameof(Id));
            if (idAttr != null)
            {
                var exteriorConfiguration =
                    new ExteriorConfiguration(Guid.Parse(idAttr.Value))
                    {
                        Name = xElement.Attribute(nameof(Name))?.Value,
                        BottomDimensions = !bool.TryParse(xElement.Attribute(nameof(BottomDimensions))?.Value, out var b) || b,
                        LeftDimensions = !bool.TryParse(xElement.Attribute(nameof(LeftDimensions))?.Value, out b) || b,
                        TopDimensions = bool.TryParse(xElement.Attribute(nameof(TopDimensions))?.Value, out b) && b,
                        RightDimensions = bool.TryParse(xElement.Attribute(nameof(RightDimensions))?.Value, out b) && b
                    };

                if (xElement.Elements(Constants.XElementName_Chain).Any())
                {
                    foreach (var element in xElement.Elements(Constants.XElementName_Chain))
                    {
                        exteriorConfiguration.Chains.Add(ExteriorDimensionChain.GetExteriorDimensionChainFromXElement(element));
                    }
                }
                else
                {
                    exteriorConfiguration.Chains.Add(new ExteriorDimensionChain());
                }

                return exteriorConfiguration;
            }

            throw new Exception(Language.GetItem(LangItem, "msg3"));
        }

        /// <summary>Получение XElement из экземпляра конфигурации для наружных стен</summary>
        /// <returns></returns>
        public XElement GetXElementFromExteriorConfigurationInstance()
        {
            var xElement = new XElement(Constants.XElementName_ExteriorConfiguration);
            xElement.SetAttributeValue(nameof(Id), Id);
            xElement.SetAttributeValue(nameof(Name), Name);
            xElement.SetAttributeValue(nameof(TopDimensions), TopDimensions);
            xElement.SetAttributeValue(nameof(BottomDimensions), BottomDimensions);
            xElement.SetAttributeValue(nameof(LeftDimensions), LeftDimensions);
            xElement.SetAttributeValue(nameof(RightDimensions), RightDimensions);

            foreach (var chain in Chains)
            {
                xElement.Add(chain.GetXElementFromDimensionChainInstance());
            }

            return xElement;
        }

        #endregion

        #region Events

        // При изменении коллекции нужно все ее элементы переименовать
        private void Chains_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Chains.Any())
            {
                for (var i = 0; i < Chains.Count; i++)
                {
                    Chains[i].DisplayName = Language.GetItem(LangItem, "msg4") + (i + 1);
                }
            }
        }

        #endregion
    }
}

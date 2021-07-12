namespace mprExteriorPlanDimensions.Body.AdvancedClasses
{
    using Autodesk.Revit.DB;
    using Enumerators;

    /// <summary>
    /// Advanced Grid
    /// </summary>
    public class AdvancedGrid
    {
        /// <summary>
        /// Revit Grid
        /// </summary>
        private readonly Grid _grid;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid">Revit grid</param>
        public AdvancedGrid(Grid grid)
        {
            _grid = grid;
            DefineAdvancedGridFields();
        }

        /// <summary>
        /// Reference
        /// </summary>
        public Reference Reference => new Reference(_grid);

        /// <summary>False - не удалось определить значения для элемента</summary>
        public bool IsDefined { get; set; } = true;

        /// <summary>Ориентация элемента</summary>
        public ElementOrientation Orientation { get; set; }

        /// <summary>Тип кривой, лежащий в основе</summary>
        public ElementCurveType CurveType { get; set; }

        /// <summary>
        /// Start Point
        /// </summary>
        public XYZ StartPoint { get; set; }

        /// <summary>
        /// End Point
        /// </summary>
        public XYZ EndPoint { get; set; }
        
        private void DefineAdvancedGridFields()
        {
            // get location curve
            var curve = _grid.Curve;
            if (curve == null)
            {
                IsDefined = false;
                return;
            }

            switch (curve)
            {
                // get curve type
                case Line _:
                    CurveType = ElementCurveType.Line;
                    break;
                case Arc _:
                    CurveType = ElementCurveType.Arc;
                    break;
                default:
                    IsDefined = false;
                    return;
            }

            // points
            StartPoint = curve.GetEndPoint(0);
            EndPoint = curve.GetEndPoint(1);
            
            // get orientation
            Orientation = GeometryHelpers.GetElementOrientation(curve);
            if (Orientation == ElementOrientation.CloseToHorizontal ||
                Orientation == ElementOrientation.CloseToVertical ||
                Orientation == ElementOrientation.Undefined)
                IsDefined = false;
        }
    }
}

namespace DTS.Framework.Designer.ViewModels
{
    public class Entity
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public string Name { get; set; }
        public string GroupName { get; set; }
    }
}
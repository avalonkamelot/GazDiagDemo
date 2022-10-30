using System.Windows.Controls;
using System.Windows.Input;

namespace TubeDemo.Controls.InfoGrid
{
    public partial class InfoGrid : UserControl
    {
        

        public InfoGridVM ViewModel { get; private set; } = new InfoGridVM();

        public InfoGrid()
        {
            DataContext = ViewModel;

            InitializeComponent();
            
            //prevent row change after edit
            DataGridItems.PreviewKeyDown += (s, e) => 
            {
                var obj = s as DataGrid;
                if(e.Key== Key.Enter 
                    && obj != null 
                    && ((DataGridRow)obj.ItemContainerGenerator.ContainerFromIndex(obj.SelectedIndex))?.IsEditing==true) 
                {
                    obj.CommitEdit();
                    e.Handled = true;
                }
            };
        }
    }

}

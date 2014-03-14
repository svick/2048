using System.Windows.Input;

namespace _2048
{
    public partial class MainWindow
    {
        private readonly Board board;

        public MainWindow()
        {
            InitializeComponent();

            board = new Board();
            DataContext = board;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            Direction? direction = null;

            switch (e.Key)
            {
            case Key.Up:
                direction = Direction.Up;
                break;
            case Key.Right:
                direction = Direction.Right;
                break;
            case Key.Down:
                direction = Direction.Down;
                break;
            case Key.Left:
                direction = Direction.Left;
                break;
            }

            if (direction != null)
                board.Move(direction.Value);
        }
    }
}
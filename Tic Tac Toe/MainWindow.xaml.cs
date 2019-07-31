using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Private Members

		/// <summary>
		/// Hold the current resoults of cells in the active game.
		/// </summary>
		private MarkType[] mResoults;
		/// <summary>
		/// True if it is player 1`s turn (x) or player2` turn (O)
		/// </summary>
		private bool mPlayer1Turn;

		/// <summary>
		/// True when game has ended;
		/// </summary>
		private bool mGameEnded;

		#endregion

		#region Constructor
		/// <summary>
		/// Defoult Constructor
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			NewGame();
		}

		#endregion
		/// <summary>
		/// Start new game  and clears all values back to the start
		/// </summary>
		private void NewGame()
		{

			//Create new array.
			mResoults = new MarkType[9];
			for (int i = 0; i < mResoults.Length; i++)
				mResoults[i] = MarkType.Null;
			//Start Player1
			mPlayer1Turn = true;

			//Intarate evry button on the grid...
			Container.Children.Cast<Button>().ToList().ForEach(button=> 
			{
				//Change background, forground  and defoult values
				button.Content = string.Empty;
				button.Background = Brushes.White;
				button.Foreground = Brushes.Blue;
			});

			//make shure the game is not finished
			mGameEnded = false;
		}
		/// <summary>
		/// Handles a button click event
		/// </summary>
		/// <param name="sender">The button was clicked</param>
		/// <param name="e">The event of the click</param>
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//start new game on the click after is finished
			if (mGameEnded)
			{
				NewGame();
				return;
			}

			//Casting the sender to a button;
			var button = (Button)sender;
			
			//find a button in array
			var column =Grid.GetColumn(button);
			var row = Grid.GetRow(button);

			var index = column + (row * 3);

			//Don`t du anything if the cell alredy has value;
			if (mResoults[index] != MarkType.Null)
				return;

			//Set value based on player
			mResoults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Zero;
			//SetButton text to resoult
			button.Content = mPlayer1Turn ? "X" : "O";

			//Change ZERO to Red
			if (!mPlayer1Turn)
			{
				button.Foreground = Brushes.Red;
			}

			//Toggle players turns
			mPlayer1Turn ^= true;

			//check for winner
			CheckForWinner();

		}
		/// <summary>
		/// Check if there is a winner opf a 3 line straight
		/// </summary>
		private void CheckForWinner()
		{
			#region horizontal_win
			//Check for horisontal values
			if (mResoults[0] != MarkType.Null && (mResoults[0] & mResoults[1] & mResoults[2]) == mResoults[0])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

			}
			if (mResoults[3] != MarkType.Null && (mResoults[3] & mResoults[4] & mResoults[5]) == mResoults[3])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

			}
			if (mResoults[6] != MarkType.Null && (mResoults[6] & mResoults[7] & mResoults[8]) == mResoults[6])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

			}
			#endregion
			#region vertical_win
			//Checl for Vertical values
			if (mResoults[0] != MarkType.Null && (mResoults[0] & mResoults[3] & mResoults[6]) == mResoults[0])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

			}
			if (mResoults[1] != MarkType.Null && (mResoults[1] & mResoults[4] & mResoults[7]) == mResoults[1])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

			}
			if (mResoults[2] != MarkType.Null && (mResoults[2] & mResoults[5] & mResoults[8]) == mResoults[2])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

			}

			#endregion
			#region diagonal_win
			//Check for diagonal valued
			if (mResoults[0] != MarkType.Null && (mResoults[0] & mResoults[4] & mResoults[8]) == mResoults[0])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

			}
			if (mResoults[2] != MarkType.Null && (mResoults[2] & mResoults[4] & mResoults[6]) == mResoults[2])
			{
				//Game ends
				mGameEnded = true;

				//Highlights winning cell in green
				Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

			}

			#endregion
			#region no_win
			//Check for no empty space left and no winner
			if (!mResoults.Any(result => result == MarkType.Null))
			{
				//Game end
				mGameEnded = true;

				//Turn all cells Orange
				Container.Children.Cast<Button>().ToList().ForEach(button =>
				{
					button.Background = Brushes.Orange;
				});

			}
			#endregion
		}

	}
}

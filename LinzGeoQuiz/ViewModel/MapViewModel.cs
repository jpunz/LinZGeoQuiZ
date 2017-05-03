using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace LinzGeoQuiz.ViewModel
{
	public class MapViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<TKCustomMapPin> _pins;
		private MapSpan _mapRegion;

		public MapViewModel()
		{
			this._pins = new ObservableCollection<TKCustomMapPin>();
            this._mapRegion = MapSpan.FromCenterAndRadius(new Position(48.286998, 14.294665), Distance.FromKilometers(5));
		}

		/// <summary>
		/// Map Clicked bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command<Position> MapClickedCommand
		{
			get
			{
				return new Command<Position>(positon =>
				{
					var pin = new TKCustomMapPin
					{
						Position = positon,
						DefaultPinColor = Color.Blue
					};

					this._pins.Clear();
					this._pins.Add(pin);
				});
			}
		}

		/// <summary>
		/// Map region bound to <see cref="TKCustomMap"/>
		/// </summary>
		public MapSpan MapRegion
		{
			get { return this._mapRegion; }
			set
			{
				if (this._mapRegion != value)
				{
					this._mapRegion = value;
					this.OnPropertyChanged("MapRegion");
				}
			}
		}

		/// <summary>
		/// Pins bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKCustomMapPin> Pins
		{
			get { return this._pins; }
			set
			{
				if (this._pins != value)
				{
					this._pins = value;
					this.OnPropertyChanged("Pins");
				}
			}
		}

		public void addSolutionPin(Position position, String street)
		{
			var pin = new TKCustomMapPin
			{
				Position = position,
				ShowCallout = true,
				DefaultPinColor = Color.Green,
				Title = "Correct Location",
				Subtitle = street
			};

			this._pins.Add(pin);
		}

		public void clearPins()
		{
			_pins.Clear();
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}

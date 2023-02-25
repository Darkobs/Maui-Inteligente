using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.Maui.Platform;

namespace MauiInteligente2022.AppBase.Controls;

public partial class RouteMap
{
	class MapCallBackHandler : Java.Lang.Object, IOnMapReadyCallback
	{
		GoogleMap googleMap;
		RouteMap routeMap;

		public MapCallBackHandler(RouteMap routeMap)
		{
			this.routeMap = routeMap;
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			this.googleMap = googleMap;
			routeMap.OnMapReady(googleMap);
		}
	}

	GoogleMap nativeMap;

	public RouteMap()
	{

	}

	internal void OnMapReady(GoogleMap googleMap)
	{
		nativeMap = googleMap;
		nativeMap.MarkerClick += NativeMap_MarkerClick;
	}

    private void NativeMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
		=> e.Marker.ShowInfoWindow();

	public void AddColorPins()
	{
		foreach(var colorPin in ColorPins) 
		{
			var marker = new MarkerOptions();
			marker.SetTitle(colorPin.Label);
			marker.SetPosition(new(colorPin.Location.Latitude, colorPin.Location.Longitude));
			marker.SetIcon(GetColorMarkerIcon(colorPin.Color));
			nativeMap.AddMarker(marker);
		}
	}

	private BitmapDescriptor GetColorMarkerIcon(Color color)
	{
		float[] hsv = new float[3];
		Android.Graphics.Color.ColorToHSV(color.ToPlatform(), hsv);
		return BitmapDescriptorFactory.DefaultMarker(hsv[0]);
	}

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

		if(Handler.PlatformView is MapView mapView)
		{
			mapView.GetMapAsync(new MapCallBackHandler(this));
		}
    }
}

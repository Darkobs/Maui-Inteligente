﻿namespace MauiInteligente2022.ViewModels;

public class AboutViewModel : BaseViewModel
{
	public AboutViewModel()
	{
		PageId = ABOUT_PAGE_ID;
		Title = Resources.AboutTitle;
		SubTitle = AppInfo.Name;
	}

	public string AppName => AppInfo.Name;

	public string AppVersion => $"{Resources.AboutAppVersion} {AppInfo.VersionString}";

	public string DeviceName => DeviceInfo.Name;

	public string DeviceManufacturer => DeviceInfo.Manufacturer;

	public string DevicePlatform => DeviceInfo.Platform.ToString();

	public string DeviceModel => DeviceInfo.Model;

	public string SystemVersion => DeviceInfo.VersionString;
}

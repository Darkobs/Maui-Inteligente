﻿using MauiInteligente2022.AppBase.Validations;
using System.Text.RegularExpressions;
using static MauiInteligente2022.AppBase.Validations.ValidationRegex;

namespace MauiInteligente2022.AppBase.Helpers;

public static class ValidationsHelper
{
	public static ValidationResult ValidateString(ValidationType validationType, string value)
	{
		if (ValidationType.None == validationType)
			return ValidationResult.Valid;

		Regex regex = validationType switch
		{
			ValidationType.Email => EmailRegex(),
			ValidationType.Empty => EmptyRegex(),
			ValidationType.Password => PasswordRegex(),
			ValidationType.Phone => PhoneRegex(),
			_ => throw new ArgumentException($"ValidationType {validationType} is not implemented)")
		};

		Match match = regex.Match( value );

		return match.Success ? ValidationResult.Valid : ValidationResult.Invalid;
	}
}

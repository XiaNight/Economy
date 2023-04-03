using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateTimeField : MonoBehaviour
{
	[SerializeField] private InputField yearInput;
	[SerializeField] private InputField monthInput;
	[SerializeField] private InputField dayInput;

	private void Awake()
	{
		// Initialize input fields to current date
		DateTime now = DateTime.Now;
		yearInput.text = now.Year.ToString();
		monthInput.text = now.Month.ToString();
		dayInput.text = now.Day.ToString();

		yearInput.onValueChanged.AddListener(delegate { OnValueChanged(); });
		monthInput.onValueChanged.AddListener(delegate { OnValueChanged(); });
		dayInput.onValueChanged.AddListener(delegate { OnValueChanged(); });
	}

	public DateTime GetDateTime()
	{
		int year = int.Parse(yearInput.text);
		int month = int.Parse(monthInput.text);
		int day = int.Parse(dayInput.text);

		return new DateTime(year, month, day);
	}

	public void SetValue(DateTime date)
	{
		yearInput.text = date.Year.ToString();
		monthInput.text = date.Month.ToString();
		dayInput.text = date.Day.ToString();
	}

	public void SetReadOnly(bool state)
	{
		yearInput.interactable = !state;
		monthInput.interactable = !state;
		dayInput.interactable = !state;
	}

	// When input value changes, check if it's valid, if not, set it to the closest valid value
	public void OnValueChanged()
	{
		int year = int.Parse(yearInput.text);
		int month = int.Parse(monthInput.text);
		int day = int.Parse(dayInput.text);

		bool isLeapYear = year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);

		// Check if year is valid
		if (year < 0)
		{
			yearInput.text = "0";
		}

		// Check if month is valid
		if (month < 1)
		{
			monthInput.text = "1";
		}
		else if (month > 12)
		{
			monthInput.text = "12";
		}

		// Check if day is valid, based on month, and leap year
		if (day < 1)
		{
			dayInput.text = "1";
		}
		else if (month == 2)
		{
			if (isLeapYear && day > 29)
			{
				dayInput.text = "29";
			}
			else if (!isLeapYear && day > 28)
			{
				dayInput.text = "28";
			}
		}
		else if (month == 4 || month == 6 || month == 9 || month == 11)
		{
			if (day > 30)
			{
				dayInput.text = "30";
			}
		}
		else
		{
			if (day > 31)
			{
				dayInput.text = "31";
			}
		}
	}
}

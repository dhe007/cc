# Currency Rate Calculator in Xamarin Forms

 This sample demonstrates how to develop a simple currency calculator application using Xamarin Forms. The app is cross-platform and can run on both Android and iOS systems.
 
# Functions

This app can query the latest currency exchange rate information online, and calculate the amount of symbol currency according to the amount of base currency entered.

# Online mode ad offline mode

The app has an offline mode and an online mode, toggled by a switch. After the latest exchange rate is queried in the online mode, it will be saved in the local database. In this way, when the network is unstable, the exchange rate information can also be obtained in the local database.

# Pattern
In the MVVM pattern, Models, Views and ViewModels are separated from each other. Views do not display logic, but interact with ViewModels in the form of data binding.

# Scalability
Deconstruct the json data to obtain a custom object type, which can flexibly access and store the required field information. The way to obtain data, the type of base currency and symbol currency can be easily switched with the bound flag. Such a program is very convenient to extend the function。


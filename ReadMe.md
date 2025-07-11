# GoChap GiftCard

GoChap lance sa dernière innovation , Une plateforme de carte cadeaux multi enseigne digital.

Cette plateforme est constituée d'une application web permettant à des souscripteurs (entreprises) de souscrire à des packages de carte cadeaux qu'ils peuvent offrir avec simplicié à des bénéficiaires (employés) .

Ces cartes cadeaux leur permettant d'effectuer des achats
par QrCode envoyés à leur adresse email ou par whatsapp ou encore directement dans l'interface mobile de leur application beneficiaire GoChap auprès de marchand acceptant ce mode de payement par le scan du QrCode depuis l'interface mobile de leur application marchande  GoChap.

# Technologies
- WebAPI de .NET pour la conception de l'API
- Blazor Server de .NET pour la conception de l'Application Web
- Flutter pour le dévéloppement des applications mobiles

# Installation
mkdir GoChapGiftCard
cd GoChapGiftCard
- API
  - git clone https://github.com/Harlequelrah/giftcard_api
  - cd giftcard_api
  - dotnet build
- Application Web
  - git clone https://github.com/Harlequelrah/blazor_giftcard
  - cd blazor giftcard
  - dotnet build
  - npm install
- Application Mobile Bénéficiaire
  - git clone https://github.com/Harlequelrah/flutter_giftcard
  - cd flutter_giftcard
  - flutter pub get
- Application Mobile Marchande
  - git clone https://github.com/Harlequelrah.flutter_merchantgiftcard
  - cd flutter_merchantgiftcard
  - flutter pub get

# Credit
- Bibliothèque .NET pour le développement de l'API
  - cd giftcard_api
  - dotnet list package
- Bibliothèque .NET pour le développement de l'application Web
  - cd blazor_giftcard
  - dotnet list package
- Biblitotheque Flutter pour le développement des applications mobiles
  - cd flutter_giftcard/flutter_merchantgiftcard
  - flutter pub deps

- Source externe
  - https://docs.flutter.dev
  - https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0

# Utilisation
- Demarrage de l'API
  - cd giftcard_api
  - dotnet watch / dotnet run / dotnet watch run

- Demarrage de l'application web
  - cd blazor_giftcard
  - dotnet watch / dotnet run / dotnet watch run

- Demarrage de l'application mobile pour Bénéficiaire
  - cd flutter_giftcard
  - flutter run (Assurez vous de disposer d'un emulateur ou d'un appareil physique)
- Demarrage de l'application mobile pour Marchand
  - cd flutter_giftcard
  - flutter run (Assurez vous de disposer d'un emulateur ou d'un appareil physique)

# Screenshots 
![image](https://github.com/user-attachments/assets/2ef40019-1579-4c5a-843b-03156c9605ca)

![image](https://github.com/user-attachments/assets/ccb7f7a1-e89b-409a-8e5c-42d7a8294c64)

![image](https://github.com/user-attachments/assets/4bee592e-0e03-4ce7-a3c4-14dbf7fa1f41)

![image](https://github.com/user-attachments/assets/bec04714-2c6a-4be4-96b2-f7ece66e9433)

![image](https://github.com/user-attachments/assets/62027968-be8d-433d-bfc8-8b1f13421222)

![image](https://github.com/user-attachments/assets/8855775b-d2e0-4b01-94c5-ac4c40660bad)

![image](https://github.com/user-attachments/assets/f31f9b09-5668-46b2-8aee-ff45245a051c)

![image](https://github.com/user-attachments/assets/9c4f7976-f649-41b7-954c-52be62dcd54c)

# Contact ou Support
Pour des questions ou du support, contactez-moi à maximeatsoudegbovi@gmail.com ou au (+228) 91 36 10 29.

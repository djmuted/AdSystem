# Użyte technologie
Ten projekt do działania wykorzystał następujące technologie:

+ Nginx - serwer HTTP
+ Nancy - framework do tworzenia aplikacji webowych za pomocą platformy .NET
+ REST - standard projektowania API
+ OpenRTB - standard obsługi Real Time Bidding
+ MySQL - system zarządzania bazami danych
+ Entity Framework Core - framework relacyjnego mapowania obiektów (ORM) dla platformy .NET Core
+ IP-API.com - darmowe API do geolokalizacji adresów IP

# Możliwości systemu
AdSystem w obecnej postaci ma następujące możliwości:

+ Rejestracja kont reklamodawców oraz reklamobiorców
+ Dodawanie i usuwanie reklam
+ Przeprowadzanie aukcji RTB
+ Wyświetlanie reklam na stronie reklamobiorcy 

# RESTful API
API dzieli się na sekcje, do których dostęp mają tylko poszczególne typy użytkowników

## API publiczne
+ POST /api/public/login/advertiser - logowanie reklamodawcy (zwraca API key)
+ POST /api/public/login/publisher - logowanie reklamobiorcy (zwraca API key)
+ POST /api/public/register/advertiser - rejestracja konta reklamodawcy (zwraca API key)
+ POST /api/public/register/publisher - rejestracja konta reklamobiorcy (zwraca API key)
+ GET /api/public/ad - zwraca kod HTML reklamy będącej zwycięzcą aukcji RTB (używany przez skrypt embedcode reklamobiorcy)

## API reklamodawcy
+ POST /api/advertiser/ads - dodawanie nowej reklamy
+ GET /api/advertiser/ads - zwraca stronicowaną listę wszystkich reklam dodanych przez reklamodawcę
+ GET /api/advertiser/ads/{guid} - zwraca informacje o pojedynczej reklamie o podanym GUID
+ DELETE /api/advertiser/ads/{guid} - usuwa reklamę o podanym guid

## API reklamobiorcy
+ GET /api/publisher/embedcode - zwraca kod do umieszczenia na stronie reklamobiorcy

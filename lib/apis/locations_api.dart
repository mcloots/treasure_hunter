import 'dart:math';

import 'package:geolocator/geolocator.dart';
import 'package:http/http.dart' as http;
import 'package:treasure_hunter/models/location_model.dart';
import 'dart:convert';

class LocationsApi {
  static String server = 'geolocation-api-kgkn.onrender.com';

  Future<List<Location>> fetchLocations(Position? userPosition) async {
    var url = Uri.https(server, '/locations');

    final response = await http.get(url);

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      List<Location> locations =
          jsonResponse.map((location) => Location.fromJson(location)).toList();

      if (userPosition != null) {
        // Calculate distances for each location
        for (var location in locations) {
          location.distance = _calculateDistance(
            userPosition.latitude,
            userPosition.longitude,
            location.latitude,
            location.longitude,
          );
        }

        // Sort locations by distance (optional)
        locations.sort((a, b) => a.distance.compareTo(b.distance));

        return locations;
      } else {
        throw Exception('Something went wrong calculating the distance');
      }
    } else {
      throw Exception('Failed to load locations');
    }
  }

  double _calculateDistance(
      double lat1, double lon1, double lat2, double lon2) {
    const p = 0.017453292519943295; // Math.PI / 180
    const c = cos;
    final a = 0.5 -
        c((lat2 - lat1) * p) / 2 +
        c(lat1 * p) * c(lat2 * p) * (1 - c((lon2 - lon1) * p)) / 2;
    return 12742 * asin(sqrt(a)); // 2 * R * asin(sqrt(a)), R = 6371 km
  }
}

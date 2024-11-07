import 'package:treasure_hunter/models/location_model.dart';

class Checkin {
  final int id;
  final int locationId;
  final String username;
  final Location location;

  Checkin({
    required this.id,
    required this.locationId,
    required this.username,
    required this.location
  });

  factory Checkin.fromJson(Map<String, dynamic> json) {
    return Checkin(
      id: json['id'],
      locationId: json['locationId'],
      username: json['username'],
      location: Location.fromJson(json['location']), // Parse nested location
    );
  }
}
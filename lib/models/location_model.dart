class Location {
  final int id;
  final String name;
  final String texture;
  final double latitude;
  final double longitude;
  double distance;

  Location({
    required this.id,
    required this.name,
    required this.texture,
    required this.latitude,
    required this.longitude,
    required this.distance
  });

  factory Location.fromJson(Map<String, dynamic> json) {
    return Location(
      id: json['id'],
      name: json['name'],
      texture: json['texture'],
      latitude: json['latitude'],
      longitude: json['longitude'],
      distance: 0
    );
  }

  Map<String, dynamic> toJson() =>
    {
      'id': id,
      'name': name,
      'texture': texture,
      'longitude': longitude,
      'latitude': latitude,
    };
}
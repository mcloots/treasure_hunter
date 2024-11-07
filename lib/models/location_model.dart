class Location {
  final int id;
  final String name;
  final String modelName;
  final double latitude;
  final double longitude;
  double distance;

  Location({
    required this.id,
    required this.name,
    required this.modelName,
    required this.latitude,
    required this.longitude,
    required this.distance
  });

  factory Location.fromJson(Map<String, dynamic> json) {
    return Location(
      id: json['id'],
      name: json['name'],
      modelName: json['modelname'],
      latitude: json['latitude'],
      longitude: json['longitude'],
      distance: 0
    );
  }
}
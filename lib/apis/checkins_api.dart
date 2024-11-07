import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:treasure_hunter/globals.dart';

import '../models/checkin_model.dart';

class CheckinsApi {
  static String server = 'geolocation-api-kgkn.onrender.com';

  static Future<List<Checkin>> fetchCheckins() async {
    final url = Uri.parse('https://$server/checkins?_expand=location&username=$globalUsername');

    final response = await http.get(url);

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      return jsonResponse.map((checkin) => Checkin.fromJson(checkin)).toList();
    } else {
      throw Exception('Failed to load checkins');
    }
  }
}

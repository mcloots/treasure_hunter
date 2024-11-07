import 'package:flutter/material.dart';
import './pages/home.dart';

void main() => runApp(const HelloFlutterApp());

class HelloFlutterApp extends StatelessWidget {
  const HelloFlutterApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Treasure Hunter',
      home: HomePage(),
    );
  }
}

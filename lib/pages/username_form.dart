import 'package:flutter/material.dart';

import '../globals.dart';

class UsernameForm extends StatefulWidget {
  const UsernameForm({super.key});

  @override
  State<StatefulWidget> createState() => _UsernameFormState();
}

class _UsernameFormState extends State<UsernameForm> {
  final TextEditingController _controller = TextEditingController();

  void _saveUsername() {
    setState(() {
      globalUsername = _controller.text;
    });
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text('Username saved: $globalUsername')),
    );
  }

  @override
  Widget build(BuildContext context) {
    if (globalUsername != null && globalUsername != "") {
      setState(() {
        _controller.text = globalUsername!;
      });
    }

    return Center(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text('Username', style: TextStyle(fontSize: 18)),
            TextField(
              controller: _controller,
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                hintText: 'Enter your username',
              ),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: _saveUsername,
              child: const Text('Save'),
            ),
          ],
        ),
      ),
    );
  }
}

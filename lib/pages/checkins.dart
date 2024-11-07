import 'package:flutter/material.dart';
import 'package:treasure_hunter/apis/checkins_api.dart';

import '../models/checkin_model.dart';

class CheckinsPage extends StatefulWidget {
  const CheckinsPage({super.key});

  @override
  State<StatefulWidget> createState() => _CheckinsPageState();
}

class _CheckinsPageState extends State<CheckinsPage> {
  List<Checkin> checkinList = [];
  int count = 0;

  @override
  void initState() {
    super.initState();
    _getCheckins();
  }

  void _getCheckins() {
    CheckinsApi.fetchCheckins().then((result) {
      setState(() {
        checkinList = result;
        count = result.length;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: _checkinItems(),
    );
  }

  ListView _checkinItems() {
    return ListView.builder(
      itemCount: count,
      itemBuilder: (BuildContext context, int position) {
        return Card(
          color: Colors.white,
          elevation: 2.0,
          child: ListTile(
            title: Text(checkinList[position].location.name),
            subtitle: Text(checkinList[position].username),
            onTap: () {
              debugPrint("Tapped on ${checkinList[position].id}");
            },
          ),
        );
      },
    );
  }
}

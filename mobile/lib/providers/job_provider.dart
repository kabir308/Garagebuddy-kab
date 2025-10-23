import 'package:flutter/foundation.dart';
import '../models/job.dart';

class JobProvider with ChangeNotifier {
  List<Job> _jobs = [];

  List<Job> get jobs => _jobs;

  // Add methods to fetch and manage jobs
}

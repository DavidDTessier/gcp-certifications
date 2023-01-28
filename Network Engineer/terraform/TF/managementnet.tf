
resource "google_compute_network" "managementnet" {
  name                    = "managementnet"
  project                 = google_project.project.project_id
  auto_create_subnetworks = false
}

resource "google_compute_subnetwork" "managementsubnet-us" {
  name          = "managementsubnet-us"
  region        = local.region
  project = google_project.project.name
  network       = google_compute_network.managementnet.self_link
  ip_cidr_range = "10.130.0.0/20"
}

resource "google_compute_firewall" "management-allow-http-ssh-rdp-icmp" {
  name    = "managementnet-allow-http-ssh-rdp-icmp"
  project = google_project.project.name
  network = google_compute_network.managementnet.self_link
  allow {
    protocol = "tcp"
    ports    = ["22", "80", "3389"]
  }
  source_ranges = ["0.0.0.0/0"]
  allow {
    protocol = "icmp"
  }
}

module "management-us-vm" {
  source              = "./instance"
  instance_name       = "management-us-vm"
  instance_zone       = local.zone
  instance_type       =  "e2-micro"
  instance_subnetwork = google_compute_subnetwork.managementsubnet-us.self_link
  instance_project = google_project.project.name

  depends_on = [
    google_project_service.compute_api
  ]
}
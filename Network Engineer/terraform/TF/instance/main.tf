variable "instance_name" {}
variable "instance_type" {}
variable "instance_zone" {}
variable "instance_subnetwork" {}
variable "instance_project" {}

resource "google_compute_instance" "name" {
  name = var.instance_name
  zone = var.instance_zone
  machine_type = var.instance_type
  project = var.instance_project
  boot_disk {
    initialize_params {
      image = "debian-cloud/debian-11"
    }
  }
  network_interface {
    subnetwork = var.instance_subnetwork
    access_config {
      
    }
  }
}

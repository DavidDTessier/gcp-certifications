resource "google_project" "project" {
  name            = "prg-demo-auto-network-tf"
  project_id      = "prg-demo-auto-network-tf"
  billing_account = "014C05-027CFB-2E6FD6"
}

resource "google_project_service" "compute_api" {
  project = google_project.project.name
  service = "compute.googleapis.com"
  timeouts {
    create = "30m"
    update = "40m"
  }

  disable_dependent_services = true
}
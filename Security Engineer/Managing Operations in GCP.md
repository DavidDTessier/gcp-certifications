# Managing GCP Migrations
There are three types of migrations:
* Lift and shift
  * moving worksloads from source to target
  * minimal modifications in order for the workloads to operate in the target environment.
  * Ex: legacy or cots applications running on VMs can be migrated to run on Compute Engine VMs
* Improve and move
  * Modernize the workload while migrating it
  * Some refactoring will occur to take advantage of cloud native offerings
* Rip and Replace
  * Decommission legacy app and redesign and rewrite the entire application to you in a cloud-native fashion

Before a migration begins, you should leverage the [Google Cloud Adoption Framework](https://cloud.google.com/adoption-framework) to determine you cloud migration roadmap.

![GCP Cloud Adoption Framework](images/migration-to-gcp-getting-started-1-gcp-adoption-framework.svg)

## Migration Path
There are four phases of the migration path.

1. Asssess
   * assessing current environment
   * take inventory of applications and their dependencies that exists
   * perform TCO calculations
   * establish benchmark app performance
2. Plan
   * create basic cloud infrastructure for you workloads to live in and plan how you will move apps
   * includes IAM, organization hierarchy, networking, prioritize migration strategy
3. Deploy
   * design and implement any changes necessary
   * execute deployment process to move the workload to gcp
   * refining the infrastructure as needed
4. Optimize
   * take full advantage of cloud-native technologies and capabilities to expand your business's potential to things such as performance, scalability, disaster recovery, costs, training, as well as opening the doors to machine learning and artificial intelligence integrations for your app.

![Migration Drivers](images/migration%20drivers.png)

# Disaster Recovery

# Backup and Recovery
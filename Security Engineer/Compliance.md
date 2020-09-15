# Google's Shared Responsibility Model

![Shared Responsibility Model](images/GCP%20Shared%20Security%20Responsiblity.png)

When an application is moved to Google Cloud Platform, Google handles many lower layers of infrastructure and security, such as Hardware, storage encryption and networking and network security. The upper layers of security remain the customer's responsibility.

## Data Access
You must control who has access to your data. This is done through Identityf Access Management (IAM) permissions, Access Control Lists (ACLs) and Firwall rules/security groups.

Another options is to expose your data through REST APIs, in this case Authentication information MUST be provided with the request in order to validate access.

## Security Assessments
Performing a vulnerability assessments or pen test against your cloud resources may be required.

GCP does not require notification to perform pen testing, however it must abide and conform to the Cloud Platform Acceptable Use Policy and the Terms of service.

GPC also provides some security assessment services such as :
    * Web Security Scanner
    * Forseti Security



# Google Security Overview
Google's phyiscal data centers are protected with a layered security model.
* Custom designed electronic access cards, biometric and metal detectors
* Vehicle access barriers
* Perimeter fencing
* Laser beam intrusion detection on data center floors
* Interior and exterior camers to detect and track intruders
* Routinely patrolled by security guards
* Limited access
  * Less than 1% of Google staff will ever set foo in a data center
* All access tracked and monitored
  * Access logs, activity records and camera footage

## Server and Software Stack Security
Google's Data Centers contain homogeneous custom-built servers with security mind:
* Reduces security footprint
* Purpose-built servers and network equiement
  * Google servers don't include unnecessary components such as video cards, chipsets, or peripheral connectors
* Stripped-down hardened version of Linus software stack
  * Continually monitored binary notifications
* Trusted server boot
  * Leveraging the Titan security chip

## Data Disposal
When data is deleted by the customer:
* The data is no longer accessible by the service and cannot be be recovered by the customer
* All relevant data will be deleted from all Google's systems in accordance with applicable laws
  * as soon as reasonably possible and within a max period of 180 days.

## Data Ownership
GCP customers own their data, not Goolge. The data will not be scanned for adverts or sold to third parties.

Google will not process the data for any purpose other than to fullfill contractual obligations.

Standard audit logs will not show when Google staff accesses customer data, using Access Tranparency you can capture and log the actions Google personnel take when accessing your data.

In order to use _Access Transparency_, you Google organization must have one of the following support levels:
* Premium
* Enterprise
* Platinum
* Gold

## Exporting Data
Data can be exported at any time without penalty, standard egress charges will apply. 

Easiest way is to leverage the Google Transfer applicance.

![Data Transfer Appliance](images/Data%20Transfer%20Appliance.png)

# Standards, Regulations and Certifications

GDPR, SOC 1/2/3, FedRAMP, HIPAA, etc see the [GCP Security Compliance Page](https://cloud.google.com/security/compliance) for more details.

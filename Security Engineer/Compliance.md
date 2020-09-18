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

## FIPS 140-2
[Federal Information Processing Standard (FIPS) Publication 140-2](https://csrc.nist.gov/csrc/media/publications/fips/140/2/final/documents/fips1402.pdf) s is a security standard that sets forth requirements for cryptographic modules, including hardware, software, and/or firmware, for U.S. federal agencies.

GCP uses a FIPS 140-2 validated encryption module called [BoringCrypto (certificate 3318)](https://csrc.nist.gov/projects/cryptographic-module-validation-program/Certificate/3318) in there production environment. This means that both data in transit to the customer and between data centers, and data at rest are encrypted using FIPS 140-2 validated encryption.

NOTE:
* Google's Local SSD does not have FIPS 140-2 validation, so you need to roll you own if required.
* Google encrypts traffic between VMs that travel between Google's data centers with NIST-approved encryption algorithms, but not FIPS
* Apps built and operated in GCP, must include there own FIPS cryptographic module implementation.

## PCI Compliance
* App Engine and Cloud Functions
  * Ingress firewall rules are available
  * Egress rules are currently not available
  * Alternative use Compute Engine and GKE
  * SAQ A-EP, SAQ-D Type merchants must provide compensating controls to meet thg requirements

* Cloud Storage
  * PAN (Primary Account Number) MUST be unreadible anywhere it is stored.
    * Leverage Cloud DLP to encrypt if needed

SAQ outlines criteria that you must address to comply with PCI DSS.

SAQ Types:
* A:
  * Merchants that have fully outsourced payment card processing to a third-party site.
  * Customers leaves your site, complete payment and return
  * Company does NOT touch card info
* A-EP:
  * Merchants that outsource payment processing to a third-party provider, but who can access customer card data at any point in the process.
* D:
  * Merchants that accept payments online and don't qualify for SAQ A or SAQ A-EP. This type includes all merchants that call a payment processor API from their own servers, regardless of tokenization.

### Payment Processing Environments
* Creating a new Google Cloud account to isolate your payment-processing environment from your production environment.
  * To simplify access restriction and compliance auditing, create a production-quality, payment-processing environment that is fully isolated from your standard production environment and any dev/QA environments 
* Restricting access to your environment.
  * Allow payment-processing environment access only to individuals who deploy your payment system code or manage your payment system machines (principle of least priviledge).
* Setting up your virtual resources.
* Designing the base Linux image that you will use to set up your app servers.
* Implementing a secure package management solution

### PCI DSS Compliance on GKE

[PCI GKE Blueprint](https://github.com/GoogleCloudPlatform/pci-gke-blueprint)

## ISO Standards

* ISO/IEC 27001:
  * outlines and provides the requirements for an information security management system (ISMS), specifies a set of best practices, and details the security controls that can help manage information risks.
* ISO/IEC 27017:
  * gives guidelines for information security controls applicable to the provision and use of cloud services by providing:
    * Additional implementation guidance for relevant controls specified in ISO/IEC 27002
    * Additional controls with implementation guidance that specifically relate to cloud services
  * provides controls and implementation guidance for both cloud service providers like Google and cloud service customers.
    * Who is responsible for what between the cloud service provider and the cloud customer
    * The removal/return of assets when a contract is terminated
    * Protection and separation of the customer’s virtual environment
    * Virtual machine configuration
    * Administrative operations and procedures associated with the cloud environment
    * Customer monitoring of activity within the cloud
    * Virtual and cloud network environment alignment
* ISO/IEC 27018:
  * relates to one of the most critical components of cloud privacy: the protection of personally identifiable information (PII).
* ISO/IEC 27701:
  * a global privacy standard that focuses on the collection and processing of personally identifiable information (PII). This standard was developed to help organizations comply with international privacy frameworks and laws, and focuses on three main factors :
    * Extends the requirements of ISO/IEC 27001 and ISO/IEC 27002 to include data privacy;
    * Provides a framework for implementing, maintaining, and continuously improving a Privacy Information Management System (PIMS);
    * Includes requirements and guidance for organizations acting as PII controllers and PII processors. 




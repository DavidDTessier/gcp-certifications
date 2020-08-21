![Logo](../images/GCP-Sec-Eng.png)

# Cloud IAM
## Overview
Identity and access management (IAM) is the practice of granting the right individuals access to the right resources for the right reasons. This series explores IAM and the individuals who are subject to it, including the following:

* **Corporate identities**: The identities that you manage for employees of your organization. These identities are used for signing in to workstations, accessing email, or using corporate applications. Corporate identities might also include non-employees such as contractors or partners that need access to corporate resources.
* **Customer identities**: The identities that you manage for users in order to interact with your website or customer-facing applications.
* **Service identities**: The identities that you manage in order to enable applications to interact with other applications or the underlying platform.

You might need to grant access to the following resources:
* Google services such as Google Cloud, Google Analytics, or G Suite
* Resources in Google Cloud, such as projects, Cloud Storage buckets, or virtual machines (VMs)
* Custom applications or resources managed by such applications

![IAM Descision Tree](images/IAM-Descision-Tree.png)

### Managing Customer Identities

Customer identities are the identities that you manage for users to let them interact with your website or customer-facing applications. Managing customer identities and their access is also referred to as customer identity and access management (CIAM).

By leveraging Google's [Identity Platfrom](https://cloud.google.com/identity-platform) you can control access to your business application. It provides Single-Sign-On (SSO) capabilities. This is done by building [authentication workflows](https://cloud.google.com/identity-platform/docs/concepts-authentication) into the application using easy-to-use SDK functionality.

#### Designing identity roles at the project and organization level

![Cloud Identity Hirearchy](images/policy-hierarchy.png)

IAM lets you set policies at the following levels of the resource hierarchy:

* Organization level. 
  * The organization resource represents your company. IAM roles granted at this level are inherited by all resources under the organization. For more information, see Access control for organizations using IAM.

* Folder level. 
  * Folders can contain projects, other folders, or a combination of both. Roles granted at the highest folder level will be inherited by projects or other folders that are contained in that parent folder. For more information, see Access control for folders using IAM.

* Project level. 
  * Projects represent a trust boundary within your company. Services within the same project have a default level of trust. For example, App Engine instances can access Cloud Storage buckets within the same project. IAM roles granted at the project level are inherited by resources within that project. For more information, see Access control for projects using IAM.

* Resource level. 
  * In addition to the existing Cloud Storage and BigQuery ACL systems, additional resources such as Genomics Datasets, Pub/Sub topics, and Compute Engine instances support lower-level roles so that you can grant certain users permission to a single resource within a project.

IAM policies are hierarchical and propagate down the structure. The effective policy for a resource is the union of the policy set at that resource and the policy inherited from its parent.

The following examples explain how policy inheritance works in practice.

**Example: Pub/Sub**
In Pub/Sub, topics and subscriptions are resources that live under a project. Assume that project_a has a topic topic_a under it. If you set a policy on project_a that grants the Editor role to bob@example.com, and set a policy on topic_a that grants the Publisher role to alice@example.com, you effectively grant the Editor role to bob@example.com and the Publisher role to alice@example.com for topic_a.

The following diagram illustrates the preceding example.

![PubSub Example](images/resource-hierarchy-pubsub1.png)

Other Examples can be found [here](https://cloud.google.com/iam/docs/resource-hierarchy-access-control)


## Service Accounts

## Cloud Identity

Cloud Identity is an Identity as a Service (IDaaS) and enterprise mobility management (EMM) product. It offers the identity services and endpoint administration that are available in G Suite as a stand-alone product. As an administrator, you can use Cloud Identity to manage your users, apps, and devices from a central location—the Google Admin console.

There are free and premium editions of Cloud Identity. [Compare the differences](https://support.google.com/cloudidentity/answer/7431902)

**If you're a G Suite admin**
* G Suite licenses are required only for users who need certain G Suite services, like Gmail. To manage users who don't need any G Suite services, you can create free Cloud Identity accounts for them.  
* In most cases, Cloud Identity Free edition users have the same identity services as G Suite users, such as single sign-on (SSO) and 2-Step Verification (2SV).

**If you're a GCP admin**
* Consumer accounts, such as personal Gmail accounts or consumer accounts with work email IDs, are unmanaged accounts and are outside of your control. If developers in your organization use unmanaged accounts to use GCP resources, you can create Cloud Identity accounts to manage these users.
* You can create free Cloud Identity accounts for each user, separate from paid G Suite accounts. By doing so, you can manage all users across your entire domain from the Google Admin console.
* Cloud Identity Free edition provides common identity services, such as SSO.

You can manage Cloud Identity via the [REST API](https://cloud.google.com/identity/docs/reference/rest)

_Cloud Identity APIs_
* [Groups API](https://cloud.google.com/identity/docs/groups)
  * Allows you to create and manage different types of groups, each of which supports different features, as well as their memberships.

Google Recommends leveraging there [Client Libraries](https://cloud.google.com/apis/docs/client-libraries-explained) in order to interact with there APIs.

Enforce multi-factor authentication
* https://cloud.google.com/identity/solutions/enforce-mfa

Enable SSO for cloud apps:
* https://cloud.google.com/identity/solutions/enable-sso

Secure corporate access on personal devices:
* https://cloud.google.com/identity/solutions/secure-corp-on-personal-devices

## Google Cloud Directory and Directory Sync

Google Cloud Directory Sync enables administrators to synchronize users, groups and other data from an Active Directory/LDAP service to their Google Cloud domain directory.

![GC Directory Sync](images/active-directory-as-idp.svg)

Google Cloud uses Google identities for authentication and access management. Manually maintaining Google identities for each employee can add unnecessary management overhead when all employees already have an account in Active Directory. By federating user identities between Google Cloud and your existing identity management system, you can automate the maintenance of Google identities and tie their lifecycle to existing users in Active Directory.

Cloud Directory Sync queries the LDAP directory to retrieve the necessary information from the directory and uses the [Directory API](https://developers.google.com/admin-sdk/directory/) to add, modify, or delete users in your Cloud Identity or G Suite account.

[Preparing your Cloud Identity or G Suit account](https://cloud.google.com/architecture/identity/federating-gcp-with-active-directory-synchronizing-user-accounts#configuring_cloud_identity)

[Configuring user provisioning](https://cloud.google.com/architecture/identity/federating-gcp-with-active-directory-synchronizing-user-accounts#configuring_user_provisioning)

#### 1.2 Managing user accounts:



* Grant roles to a Google group instead of to individual users when possible. It is easier to manage members in a Google group than to update an IAM policy. Make sure to control the ownership of the Google group used in IAM policies.

* Use the security principle of least privilege to grant IAM roles; that is, only give the least amount of access necessary to your resources.

* Grant roles at the smallest scope needed. For example, if a user only needs access to publish messages to a Pub/Sub topic, grant the Publisher role to the user for that topic. **_Remember that the policies for child resources inherit from the policies for their parent resources. For example, if the policy for a project grants a user the ability to administer Compute Engine virtual machine (VM) instances, then the user can administer any Compute Engine VM in that project, regardless of the policy you set on each VM. If you need to grant a role to a user or group that spans across multiple projects, set that role at the folder level instead of setting it at the project level._**

* Use labels to annotate, group, and filter resources.

* Audit your policies to ensure compliance. Audit logs contain all setIamPolicy() calls, so you can trace when a policy has been created or modified.

* Audit the ownership and the membership of the Google groups used in policies.

* If you want to limit project creation in your organization, change the organization access policy to grant the Project Creator role to a group that you manage.

#### User Life Cycle Management Process

User lifecycle management—the ability to automatically add and remove users to applications—is one of the key features of [Cloud Identity](https://cloud.google.com/identity), Google Cloud’s identity, access, and device management solution, will allow you to provision and de-provision users and provide them access to a multitude of third-party applications directly from the Cloud Identiy or GSuite Admin Console. 

This is done by enabling Single-Sign-On (SSO) on the application to use Google Cloud Identity as the Identity Provider (IdP)

### Managing Service Accounts
When using an application to access Cloud Platform APIs, we recommend you use a service account, an identity whose credentials your application code can use to access other GCP services. You can access a service account from code running on GCP, in your on-premises environment or even another cloud.

Service accounts differ from user accounts in a few key ways:

  * Service accounts do not have passwords, and cannot log in via browsers or cookies.
  * Service accounts are associated with private/public RSA key-pairs that are used for authentication to Google.
  * IAM permissions can be granted to allow other users (or other service accounts) to impersonate a service account.
  * Service accounts are not members of your G Suite domain, unlike user accounts. For example, if you share assets with all members in your G Suite domain, they will not be shared with service accounts. Similarly, any assets created by a service account cannot be owned or managed by G Suite or Cloud Identity admins. This doesn't apply when using domain-wide delegation, because API calls are authorized as the impersonated user, not the service account itself.

GCP provides the [IAM Recommender](https://cloud.google.com/iam/docs/recommender-overview) tool for providing insights and recommendations on enforcing least priviledge on you IAM accounts.

**Best Practices**
Best practice for managing service accounts can be found [here](https://cloud.google.com/blog/products/gcp/help-keep-your-google-cloud-service-account-keys-safe) and [here](https://cloud.google.com/iam/docs/understanding-service-accounts#best_practices) also.

One key best practice for is for key rotation is to setup a cron job to rotate keys and store them in a Cloud Storage Bucket which developers have READ access to and have them download the updated keys daily from there.

**GCloud SDK Snippets**
Use _**add-iam-policy-binding**_ to add a role to a member 
_`gcloud group add-iam-policy-binding resource --member=member --role=role-id`_

**Example**:
```
gcloud projects add-iam-policy-binding my-project --member=user:my-user@example.com --role=roles/viewer 
```

Use _**remove-iam-policy-binding**_ to remove a role from a member
_`gcloud group remove-iam-policy-binding resource --member=member --role=role-id`_
**Example:**
```
gcloud projects remove-iam-policy-binding my-project --member=user:my-user@example.com --role=roles/viewer
```

There are two types of service account keys:
* GCP-managed keys. 
  * These keys are used by Cloud Platform services such as App Engine and Compute Engine. They cannot be downloaded, and are automatically rotated and used for signing for a maximum of two weeks. The rotation process is probabilistic; usage of the new key will gradually ramp up and down over the key's lifetime. We recommend caching the public key set for a service account for at most 24 hours to ensure that you always have access to the current key set.
* User-managed keys. 
  * These keys are created, downloadable, and managed by users. They expire 10 years from creation, and cease authenticating successfully when they are deleted from the service account.
    * https://cloud.google.com/iam/docs/understanding-service-accounts#managing_service_account_keys

Details on auditing service accounts and keys
can be found [here](https://cloud.google.com/iam/docs/audit-logging).

Automating the rotation of user-managed service account keys can be done leveraging the [Keyrotator python app](https://github.com/Googlecloudplatform/keyrotator)

Typically, service accounts are used in scenarios such as:

* Running workloads on virtual machines (VMs).
* Running workloads on on-premises workstations or data centers that call Google APIs.
* Running workloads which are not tied to the lifecycle of a human user.

**GCloud CLI Snippets**
Creating a service account:

```
gcloud iam service-accounts create sa-name --description="sa-description" --display-name="sa-display-name"
```

Creating a service account key:

```
gcloud iam service-accounts keys create ~/key.json --iam-account sa-name@project-id.iam.gserviceaccount.com
```

This command generates a key json that will look like the following:

```
{
  "type": "service_account",
  "project_id": "project-id",
  "private_key_id": "key-id",
  "private_key": "-----BEGIN PRIVATE KEY-----\nprivate-key\n-----END PRIVATE KEY-----\n",
  "client_email": "service-account-email",
  "client_id": "client-id",
  "auth_uri": "https://accounts.google.com/o/oauth2/auth",
  "token_uri": "https://accounts.google.com/o/oauth2/token",
  "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
  "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/service-account-email"
}
```

To updating a Service Account use the following command:
```
gcloud iam service-accounts update sa-name@project-id.iam.gserviceaccount.com --description="updated-sa-description" --display-name="updated-display-name"
```

To disable a service account use the following command:

```
gcloud iam service-accounts disable sa-name@project-id.iam.gserviceaccount.com
```

To enable a service account use the following command:

```
gcloud iam service-accounts enable sa-name@project-id.iam.gserviceaccount.com
```

To delete a service account use the following command:

```
gcloud iam service-accounts delete sa-name@project-id.iam.gserviceaccount.com
```
In some cases you can use the `undelete` command to _undelete_ a deleted service account. 

These scenerios are:

* The service account was deleted less than 30 days ago.
  * After 30 days, IAM permanently removes the service account. Google Cloud cannot recover the service account after it is permanently removed, even if you file a support request.

* The service account was deleted less than 30 days ago.

After 30 days, IAM permanently removes the service account. Google Cloud cannot recover the service account after it is permanently removed, even if you file a support request.

* There is no existing service account with the same name as the deleted service account.
  * For example, suppose that you accidentally delete the service account my-service-account@project-id.iam.gserviceaccount.com. You still need a service account with that name, so you create a new service account with the same name, my-service-account@project-id.iam.gserviceaccount.com. The new service account does not inherit the permissions of the deleted service account. In effect, it is completely separate from the deleted service account. However, you cannot undelete the original service account, because the new service account has the same name.

* To address this issue, delete the new service account, then try to undelete the original service account.

If you are not able to undelete the service account, you can create a new service account with the same name; revoke all of the roles from the deleted service account; and grant the same roles to the new service account. For details, see Policies with deleted members.


### Managing Authentication

12 Best Practices on managing passwords - [here](https://cloud.google.com/blog/products/gcp/12-best-practices-for-user-account)


Set up password policy for users following the best practices found [here](https://cloud.google.com/solutions/modern-password-security-for-users.pdf) as well as best practices for system/app authentication [here](https://cloud.google.com/solutions/modern-password-security-for-system-designers.pdf).

**When password policies don't apply**
* You can update user passwords as a hash by using the bulk user upload tool or the G Suite Password Sync tool. However, if you apply password policies to an entire organizational unit and then upload passwords as a hash for a subset of users in that unit, the policies are not enforced for that subset of users. For details, see the G Suite Admin SDK and About G Suite Password Sync.
* Password policies don't apply to any user passwords that you reset manually. If you manually reset a password, make sure to select Enforce password policy at next sign-in for that user.
* The password policies you configure don't apply to users who are authenticated on a third-party identity provider (IdP) using SAML.

Enabling SSO for Cloud Apps [here](https://cloud.google.com/identity/solutions/enable-sso)

Configuring and enform Two-Factor Auth (2mfa): https://cloud.google.com/identity/solutions/enforce-mfa

If Cloud Identity is your identity provider (IdP), you can implement 2SV in several ways. If you use a third-party IdP, check with them about their 2SV offering.

You can select different levels of 2SV enforcement:

* Optional—employee decides if they will use 2SV.
* Mandatory—employee chooses the 2SV method.
* Mandatory security keys—employee must use a security key.

### Managing Authorization and Access Control





Best practices for IAM Security can be found [here](https://cloud.google.com/iam/docs/using-iam-securely)






#### Managing IAM permissions with primitive, predefined, and custom roles

There are three kinds of roles in IAM:

* Primitive roles: 
  * Roles historically available in the Google Cloud Console. These roles are Owner, Editor, and Viewer. Avoid using these roles if possible, because they include a wide range of permissions across all Google Cloud services.

* Predefined roles: 
  * Roles that give finer-grained access control than the primitive roles. For example, the predefined role Pub/Sub Publisher (roles/pubsub.publisher) provides access to only publish messages to a Pub/Sub topic.

* Custom roles: 
  * Roles that you create to tailor permissions to the needs of your organization when predefined roles don't meet your needs.

GKE IAM Predefined Roles - [here](https://cloud.google.com/kubernetes-engine/docs/how-to/iam#predefined)
GCE IAM Policies - [here](https://cloud.google.com/compute/docs/access#resource-policies)


### GC Storage IAMs vs ACLs

Cloud IAM and ACLs work in tandem to grant access to your buckets and objects: a user only needs permission from either Cloud IAM or an ACL to access a bucket or object. 
In general, permissions granted by Cloud IAM policies do not appear in ACLs, and permissions granted by ACLs do not appear in Cloud IAM policies.

In most cases IAMs are the recommended approach for giving access to resources.

* ACLs
  * An access control list (ACL) is a mechanism you can use to define who has access to your buckets and objects, as well as what level of access they have. In Cloud Storage, you apply ACLs to individual buckets and objects. Each ACL consists of one or more entries. An entry gives a specific user (or group) the ability to perform specific actions. Each entry consists of two pieces of information:

    * A **permission**, which defines what actions can be performed (for example, read or write).
    * A **scope** (sometimes referred to as a grantee), which defines who can perform the specified actions (for example, a specific user or group of users).

* Legacy Bucket Cloud IAM roles work in tandem with bucket ACLs: when you add or remove a Legacy Bucket role, the ACLs associated with the bucket reflect your changes. Similarly, changing a bucket-specific ACL updates the corresponding Legacy Bucket role for the bucket.
  * _(IAM) Storage Legacy Bucket Reader = (ACL)Bucket Reader_
  * _(IAM) Storage Legacy Bucket Writer = (ACL) Bucket Writer_
  * _(IAM) Storage Legacy Bucket Owner = (ACL) Bucket Owner_

* IAMs
  * https://cloud.google.com/storage/docs/access-control/iam#overview
  * https://cloud.google.com/storage/docs/access-control/iam-permissions
  * Cloud IAM allows you to control who has access to the resources in your Google Cloud project. Resources include Cloud Storage buckets and objects stored within buckets, as well as other Google Cloud entities such as Compute Engine instances.
  * The set of access rules you apply to a resource is called a Cloud IAM policy. A Cloud IAM policy applied to your project defines the actions that users can take on all objects or buckets within your project. A Cloud IAM policy applied to a single bucket defines the actions that users can take on that specific bucket and objects within it.
    * For example, you can create a Cloud IAM policy for one of your buckets that gives one user administrative control of that bucket. Meanwhile, you can add another user to your project-wide Cloud IAM policy that gives that user the ability to view objects in any bucket of your project.
  * Roles are a bundle of one or more permissions. For example, the Storage Object Viewer role contains the permissions storage.objects.get and storage.objects.list. You assign roles to members, which allows them to perform actions on the buckets and objects in your project.
  * Granting roles at the bucket level does not affect any existing roles that you granted at the project level, and vice versa. For example, say you want to give a user permission to read objects in any bucket but create objects only in one specific bucket. To achieve this, give the user the Storage Object Viewer role at the project level, thus allowing the user to read any object stored in any bucket in your project, and give the user the Storage Object Creator role at the bucket level for a specific bucket, thus allowing the user to create objects only in that bucket. Some roles can be used at both the project level and the bucket level. When used at the project level, the permissions they contain apply to all buckets and objects in the project. When used at the bucket level, the permissions only apply to a specific bucket and the objects within it. Examples of such roles are Storage Admin, Storage Object Viewer, and Storage Object Creator. Some roles can only be applied at one level. For example, you can only apply the Viewer role at the project level, while you can only apply the Storage Legacy Object Owner role at the bucket level.
  * All other bucket-level Cloud IAM roles, and all project-level Cloud IAM roles, work independently from ACLs. For example, if you give a user the Storage Object Viewer role, the ACLs remain unchanged. 




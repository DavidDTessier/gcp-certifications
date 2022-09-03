![Logo](../images/GCP-Sec-Eng.png)
# Google Cloud - Cloud Security Engineer Certification

[^1] A Professional Cloud Security Engineer enables organizations to design and implement a secure infrastructure on Google Cloud Platform. Through an understanding of security best practices and industry security requirements, this individual designs, develops, and manages a secure infrastructure leveraging Google security technologies. The Cloud Security Professional should be proficient in all aspects of Cloud Security including managing identity and access management, defining organizational structure and policies, using Google technologies to provide data protection, configuring network security defenses, collecting and analyzing Google Cloud Platform logs, managing incident responses, and an understanding of regulatory concerns.

The Professional Cloud Security Engineer exam assesses your ability to:

* Configure access within a cloud solution environment
* Configure network security
* Ensure data protection
* Manage operations within a cloud solution environment
* Ensure compliance

Link to the Exam Guide: [Sec Engineer Exam](https://cloud.google.com/certification/guides/cloud-security-engineer)

# Section 1. Configuring access with a cloud solution environment

<details>
<summary> 1.1 Configuring Cloud Idenity</summary>

## 1.1.a - Managing Cloud identity
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

The following diagram illustrates the flow for integrate you existing Identity and Access Management system.

![IdP](images/integrating_your_current_identity_manageme.max-1500x1500.png)

* Grant roles to a Google group instead of to individual users when possible. It is easier to manage members in a Google group than to update an IAM policy. Make sure to control the ownership of the Google group used in IAM policies.

* Use the security principle of least privilege to grant IAM roles; that is, only give the least amount of access necessary to your resources.

* Grant roles at the smallest scope needed. For example, if a user only needs access to publish messages to a Pub/Sub topic, grant the Publisher role to the user for that topic. **_Remember that the policies for child resources inherit from the policies for their parent resources. For example, if the policy for a project grants a user the ability to administer Compute Engine virtual machine (VM) instances, then the user can administer any Compute Engine VM in that project, regardless of the policy you set on each VM. If you need to grant a role to a user or group that spans across multiple projects, set that role at the folder level instead of setting it at the project level._**

* Use labels to annotate, group, and filter resources.

* Audit your policies to ensure compliance. Audit logs contain all `setIamPolicy()` calls, so you can trace when a policy has been created or modified.

* Audit the ownership and the membership of the Google groups used in policies.

* If you want to limit project creation in your organization, change the organization access policy to grant the Project Creator role to a group that you manage.

### Perform G Suite Domain-Wide Delegation of Authority
In enterprise applications you may want to programmatically access a user's data without any manual authorization on their part. In G Suite domains, the domain administrator can grant third-party applications with domain-wide access to its users' data — this is known as domain-wide delegation of authority. To delegate authority this way, domain administrators can use service accounts with OAuth 2.0.

To access user data on a G Suite domain, the service account that you created needs to be granted access by a super administrator for the domain.

OAuth Scopes:
For example, if you require domain-wide access to Users and Groups enter: https://www.googleapis.com/auth/admin.directory.user, https://www.googleapis.com/auth/admin.directory.group


## 1.1.b - Configuring Google Cloud Directory Sync
Google Cloud Directory Sync enables administrators to synchronize users, groups and other data from an Active Directory/LDAP service to their Google Cloud domain directory.

You need to enable Cloud Platform to recognize your users, using one of two recommended methods: Google Cloud directory sync (shown on the left below), or third-party identity provider connectors to Cloud Identity (shown on the right below).

![Sync Identities](images/Sync_identities.max-1100x1100_m0h2F9X.max-1100x1100.png)

![GC Directory Sync](images/active-directory-as-idp.svg)

Google Cloud uses Google identities for authentication and access management. Manually maintaining Google identities for each employee can add unnecessary management overhead when all employees already have an account in Active Directory. By federating user identities between Google Cloud and your existing identity management system, you can automate the maintenance of Google identities and tie their lifecycle to existing users in Active Directory.

Cloud Directory Sync queries the LDAP directory to retrieve the necessary information from the directory and uses the [Directory API](https://developers.google.com/admin-sdk/directory/) to add, modify, or delete users in your Cloud Identity or G Suite account.

[Preparing your Cloud Identity or G Suit account](https://cloud.google.com/architecture/identity/federating-gcp-with-active-directory-synchronizing-user-accounts#configuring_cloud_identity)

[Configuring user provisioning](https://cloud.google.com/architecture/identity/federating-gcp-with-active-directory-synchronizing-user-accounts#configuring_user_provisioning)

## 1.1.c - Managing super administrator account

Best practices for super admin can be found [here](https://cloud.google.com/resource-manager/docs/super-admin-best-practices).

## Super Admin Best Practices

Super admin accounts have irrevocable administrative permissions that we do not recommended using in the day-to-day administration of your organization. Best practices for managing super admins are as follows:

* Create a super admin email address
  * Create a new email address that is not specific to a particular user as the G Suite or Cloud Identity super admin account. This account should be further secured with multi-factor authentication, and could be used as an emergency recovery tool.

* Designate organization admins
  * This role has a smaller set of permissions that are designed to manage your day to day organization operations. You should also create a private Google Cloud administrator group in your G Suite or Cloud Identity super admin account. Add your organization administrator users to this group, but not your super admin user. Grant this group the Organization Administrator IAM role or a limited subset of the role's permissions.
  * We recommend keeping your super admin account separate from your organization administrator group.

* Set appropriate roles
* Discourage the use of Super Admin
  * Enforce multi-factor authentication on your super admin accounts as well as all accounts that have elevated privileges.
  * Use a security key or other physical authentication device to enforce two-step verification.
  * For the initial super admin account, ensure that the security key is kept in a safe place, preferably at your physical location.

* Setup Multiple Super Admin Accounts
  * A business should have more than one super admin account, each managed by a separate individual. If one account is lost or compromised, another super admin can perform critical tasks while the other account is recovered.

## 1.1.d - Automating user lifecycle management process

User lifecycle management—the ability to automatically add and remove users to applications—is one of the key features of [Cloud Identity](https://cloud.google.com/identity), Google Cloud’s identity, access, and device management solution, will allow you to provision and de-provision users and provide them access to a multitude of third-party applications directly from the Cloud Identiy or GSuite Admin Console.

This is done by enabling Single-Sign-On (SSO) on the application to use Google Cloud Identity as the Identity Provider (IdP)

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



## 1.1.e - Administering user accounts and groups programmatically

Use the [Directory API](https://developers.google.com/admin-sdk/directory/) within the Admin SDK to add, modify, or delete users in your Cloud Identity or G Suite account.


The following script leverages the directory api within the Admin SDK add a new user:
```
/**
 * Adds a new user to the domain, including only the required information. For
 * the full list of user fields, see the API's reference documentation:
 * @see https://developers.google.com/admin-sdk/directory/v1/reference/users/insert
 */
function addUser() {
  let user = {
    // TODO (developer) - Replace primaryEmail value with yours
    primaryEmail: 'liz@example.com',
    name: {
      givenName: 'Elizabeth',
      familyName: 'Smith'
    },
    // Generate a random password string.
    password: Math.random().toString(36)
  };
  try {
    user = AdminDirectory.Users.insert(user);
    Logger.log('User %s created with ID %s.', user.primaryEmail, user.id);
  } catch (err) {
    // TODO (developer)- Handle exception from the API
    Logger.log('Failed with error %s', err.message);
  }
}

```

More samples can be found [here](https://developers.google.com/apps-script/advanced/admin-sdk-directory).


</details>
<details>
<summary> 1.2 Managing service accounts</summary>

## 1.2.a - Protecting and auditing service account keys

Details on auditing service accounts and keys
can be found [here](https://cloud.google.com/iam/docs/audit-logging).



## 1.2.b - Automating the rotation of user-managed service account keys
Automating the rotation of user-managed service account keys can be done leveraging the [Keyrotator python app](https://github.com/Googlecloudplatform/keyrotator)

![sa key rotation vault](https://miro.medium.com/max/1400/0*0NB2VCIYxFw0vQK1)

One key best practice for key rotation is to setup a cron job to rotate keys and store them in a Cloud Storage Bucket which developers have READ access to and have them download the updated keys daily from there.

You Can also leverage [Hashicorp Vault](https://www.vaultproject.io/) for managing service account & keys. See [here](https://medium.com/techking/key-rotation-in-google-cloud-3ee8ff0a7828#:~:text=%E2%80%9CA%20security%20best%20practice%20is,keys) as well as [official google documentation](https://cloud.google.com/blog/products/identity-security/how-you-and-wepay-can-use-hashicorp-vault) for more details

### Hashicorp Vault

First enable the secrets engine:

`$ vault secrets enable gcp`

Then setup the engine with initial config and role sets:
```
$ vault write gcp/config \
credentials=@path/to/creds.json \
 ttl=3600 \
 max_ttl=86400
```
This config supplies default credentials that Vault will use to generate the service account keys and access tokens, as well as TTL metadata for the leases Vault assigns to these secrets when generated.

Role sets define the sets of IAM roles, bound to specific resources, that you assign to generated credentials. Each role set can generate one of two types of secrets: either `access_token` for one-use OAuth access tokens or `service_account_key` for long-lived service account keys. Here are some examples for both types of rolesets:

```
# Create role sets
$ vault write gcp/roleset/token-role-set \
    project="myproject" \
    secret_type="access_token" \
    bindings=@token_bindings.hcl
    token_scopes="https://www.googleapis.com/auth/cloud-platform"
$ vault write gcp/roleset/key-role-set \
    project="myproject" \
    secret_type="service_account_key"
    bindings=””
```

The above bindings param expects a string (or, using the special Vault syntax ‘@’, a path to a file containing this string) with the following HCL (or JSON) format

```
resource "path/to/my/resource" {
    roles = [
      "roles/viewer",
      "roles/my-other-role",
    ]
}
resource "path/to/another/resource" {
    roles = [
      "roles/editor",
    ]
}
```

Creating a new role set generates a new service account for a role set. When a user generates a set of credentials, they specify a role set (and thus service account) under which to create the credentials.

Once you have set up the secrets engine, a Vault client can easily generate new secrets:

```
$ vault read gcp/key/key-role-set
Key                 Value
---                 -----
lease_id            gcp/key/key-roleset/
lease_duration      1h
lease_renewable     true
key_algorithm       KEY_ALG_RSA_2048
key_type            TYPE_GOOGLE_CREDENTIALS_FILE
private_key_data    
$ vault read gcp/token/token-role-set
Key                 Value
---                 -----
lease_id           gcp/token/test/
lease_duration     59m59s
lease_renewable    false
token              ya29.c.restoftoken...
```

These credentials can then be used to make calls to GCP APIs as needed and can be automatically revoked by Vault.

To learn more, check out the [GCP IAM service account secret engine documentation](https://www.vaultproject.io/docs/secrets/gcp/index.html).

## 1.2.c - Identifying scenarios requiring service accounts
Typically, service accounts are used in scenarios such as:

* Running workloads on virtual machines (VMs).
* Running workloads on on-premises workstations or data centers that call Google APIs.
* Running workloads which are not tied to the lifecycle of a human user.

## 1.2.d - Creating, authorizing, and securing service accounts

When using an application to access Cloud Platform APIs, we recommend you use a service account, an identity whose credentials your application code can use to access other GCP services. You can access a service account from code running on GCP, in your on-premises environment or even another cloud.

Service accounts differ from user accounts in a few key ways:

  * Service accounts do not have passwords, and cannot log in via browsers or cookies.
  * Service accounts are associated with private/public RSA key-pairs that are used for authentication to Google.
  * IAM permissions can be granted to allow other users (or other service accounts) to impersonate a service account.
  * Service accounts are not members of your G Suite domain, unlike user accounts. For example, if you share assets with all members in your G Suite domain, they will not be shared with service accounts. Similarly, any assets created by a service account cannot be owned or managed by G Suite or Cloud Identity admins. This doesn't apply when using domain-wide delegation, because API calls are authorized as the impersonated user, not the service account itself.

GCP provides the [IAM Recommender](https://cloud.google.com/iam/docs/recommender-overview) tool for providing insights and recommendations on enforcing least priviledge on you IAM accounts.

Once you decide that you need a service account, you can ask yourself the following questions to understand how you're going to use the service account:

* What resources can the service account access?
* What permissions does the service account need?
* Where will the code that assumes the identity of the service account be running: on Google Cloud Platform or on-premises?

Use the following diagram to help answer the above questions:

![sa flow](images/sa-flowchart.png)

There are two types of service account keys:
* GCP-managed keys.
  * These keys are used by Cloud Platform services such as App Engine and Compute Engine. They cannot be downloaded, and are automatically rotated and used for signing for a maximum of two weeks. The rotation process is probabilistic; usage of the new key will gradually ramp up and down over the key's lifetime. We recommend caching the public key set for a service account for at most 24 hours to ensure that you always have access to the current key set.
* User-managed keys.
  * These keys are created, downloadable, and managed by users. They expire 10 years from creation, and cease authenticating successfully when they are deleted from the service account.
    * [Managing Service Account Keys](https://cloud.google.com/iam/docs/understanding-service-accounts#managing_service_account_keys)

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

To updated a Service Account use the following command:
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


**Best Practices**
Best practice for managing service accounts can be found [here](https://cloud.google.com/blog/products/gcp/help-keep-your-google-cloud-service-account-keys-safe) and [here](https://cloud.google.com/iam/docs/understanding-service-accounts#best_practices) also.



## 1.2.3 - Securely managing API access management
## 1.2.f - Managing and creating short-lived credentials

Can you vault to create short live credentials or use the [following](https://cloud.google.com/iam/docs/create-short-lived-credentials-direct).

The **Service Account Token Creator role** lets principals impersonate service accounts to do the following:

* Create OAuth 2.0 access tokens, which you can use to authenticate with Google APIs
* Create OpenID Connect (OIDC) ID tokens
* Sign JSON Web Tokens (JWTs) and binary blobs so that they can be used for authentication


Key IAM Permissions for Services Accounts:

    * `iam.serviceAccounts.actAs`:
      * attach it so resources that will authenticate as the service account
      * process that perform long running jobs
    * `iam.serviceAccounts.getAccessToken` (for OAuth 2.0 access tokens) and
      `iam.serviceAccounts.getOpenIdToken` (for OpenID Connect(OICD) ID Tokens)
    `iam.serviceAccounts.signBlob` and
    `iam.serviceAccounts.signJwt` and
    `iam.serviceAccounts.implicitDelegation` and also the role `roles/iam.serviceAccountTokenCreator`
      * a user (or service) can directly impersonate (or assert) the identity of a service account in a few common scenarios


**Impersonate a service account**

```
# serviceAccount:ansible  impersonate as a svc account terraform@${PROJECT_ID}.iam.gserviceaccount.com

# ${SA_PROJECT_ID} is the global project storing all the service accounts
TF_SA_EMAIL=terraform@${SA_PROJECT_ID}.iam.gserviceaccount.com
ANSIBLE_SA_EMAIL="ansible@${SA_PROJECT_ID}.iam.gserviceaccount.com"

gcloud iam service-accounts add-iam-policy-binding ${TF_SA_EMAIL} \
    --project ${SA_PROJECT_ID} \
    --member "serviceAccount:$ANSIBLE_SA_EMAIL" \
    --role roles/iam.serviceAccountTokenCreator

# create a gcp project $A_PROJECT_ID under $A_FOLDER_ID
gcloud projects --impersonate-service-account=$TF_SA_EMAIL create $A_PROJECT_ID --name=$A_PROJECT_NAME --folder=$A_FOLDER_ID

# user:pythonrocks@gmail.com impersonate as a svc account terraform@${PROJECT_ID}.iam.gserviceaccount.com
TF_SA_EMAIL=terraform@your-service-account-project.iam.gserviceaccount.com

gcloud iam service-accounts add-iam-policy-binding  $TF_SA_EMAIL --member=user:pythonrocks@gmail.com \
--role roles/iam.serviceAccountTokenCreator

gcloud container clusters list --impersonate-service-account=terraform@${PROJECT_ID}.iam.gserviceaccount.com

```

See more details [here](https://cloud.google.com/iam/docs/create-short-lived-credentials-direct)

**Workload Identity Federation**

You can also grant identities from a workload that runs outside of Google Cloud, such as on Amazon Web Services (AWS) or Microsoft Azure, the ability to impersonate a service account. This lets you access resources directly, using short-lived credentials, instead of using a service account key.

You can use a workload identity pool to organize and manage external identities.

A project can contain multiple workload identity pools, and each pool can have access to different resources. This lets you follow the principle of least privilege by grouping related identities in the same pool, and then granting them fine-grained access to resources.

In general, we recommend creating a new pool for each non-Google Cloud environment that needs to access Google Cloud resources, such as development, staging, or production environments.

Workload identity pool providers
A workload identity pool provider is an entity that describes a relationship between Google Cloud and an external identity provider, such as the following:
  * AWS
  * Azure Active Directory
  * On-premises Active Directory
  * Okta
  * Kubernetes clusters

Workload identity federation follows the OAuth 2.0 token exchange specification. You provide a credential from an external identity provider to the [Security Token Service](https://cloud.google.com/iam/docs/reference/sts/rest), which verifies the identity on the credential, and then returns a federated token in exchange.

To impersonate a service account you need to grant the external entity the Workload Identity User role (`roles.iam.workloadIdentityUser`) on a service account with roles required for the workload.

**Single Identity**
```
principal://iam.googleapis.com/projects/PROJECT_NUMBER/locations/global/workloadIdentityPools/POOL_ID/subject/SUBJECT_NAME
```

**All identities in a group**
```
principalSet://iam.googleapis.com/projects/PROJECT_NUMBER/locations/global/workloadIdentityPools/POOL_ID/group/GROUP_NAME
```

**All identities with a specific attribute value**
```
principalSet://iam.googleapis.com/projects/PROJECT_NUMBER/locations/global/workloadIdentityPools/POOL_ID/attribute.ATTRIBUTE_NAME/ATTRIBUTE_VALUE
```

**All identities in a pool**
```
principalSet://iam.googleapis.com/projects/PROJECT_NUMBER/locations/global/workloadIdentityPools/POOL_ID/*
```

</details>


<details>
<summary> 1.3 Managing Authentication</summary>

## 1.3.a - Creating a password policy for user accounts
12 Best Practices on managing passwords - [here](https://cloud.google.com/blog/products/gcp/12-best-practices-for-user-account)


Set up password policy for users following the best practices found [here](https://cloud.google.com/solutions/modern-password-security-for-users.pdf) as well as best practices for system/app authentication [here](https://cloud.google.com/solutions/modern-password-security-for-system-designers.pdf).

**When password policies don't apply**
* You can update user passwords as a hash by using the bulk user upload tool or the G Suite Password Sync tool. However, if you apply password policies to an entire organizational unit and then upload passwords as a hash for a subset of users in that unit, the policies are not enforced for that subset of users. For details, see the G Suite Admin SDK and About G Suite Password Sync.
* Password policies don't apply to any user passwords that you reset manually. If you manually reset a password, make sure to select Enforce password policy at next sign-in for that user.
* The password policies you configure don't apply to users who are authenticated on a third-party identity provider (IdP) using SAML.

Enabling SSO for Cloud Apps [here](https://cloud.google.com/identity/solutions/enable-sso)


## 1.3.b - Establishing a Security Assertion Markup Language (SAML)

Signing in users with SAML:https://cloud.google.com/identity-platform/docs/web/saml


## 1.3.c - Configuring and enforcing two-factor authentication

Configuring and enfocing Two-Factor Auth(2mfa): https://cloud.google.com/identity/solutions/enforce-mfa

If Cloud Identity is your identity provider (IdP), you can implement 2SV in several ways. If you use a third-party IdP, check with them about their 2SV offering.

You can select different levels of 2SV enforcement:

* Optional—employee decides if they will use 2SV.
* Mandatory—employee chooses the 2SV method.
* Mandatory security keys—employee must use a security key.

</details>

<details>
<summary> 1.4 Managing and implementing authorization controls </summary>

## 1.4.a - Managing privileged roles and separation of duties

Best practices for IAM Security can be found [here](https://cloud.google.com/iam/docs/using-iam-securely)

**Cloud IAM best Practices**
* Use groups when configuring GCP access
* Assign roles to the groups instead of individual users
* Utilizing predifined roles offers less admin overhead
* Prededinfed roles are managed by Google
* Custom roles are **NOT** managed by Google
* Audit logs record project-level permission changes
* Audit policy changes
* Export audit logs to Cloud Storage to store logs for longer periods of time
* Service Account Key Rotation
  * Create a new key, update app config, delete old key

The following command lists all grantable roles for a given resource.
```
gcloud iam list-grantable-roles [full-resource-name]
```

## 1.4.b - Managing IAM permissions with basic, predefined, and custom roles

There are three kinds of roles in IAM:

* Primitive roles:
  * Roles historically available in the Google Cloud Console. These roles are Owner, Editor, and Viewer. Avoid using these roles if possible, because they include a wide range of permissions across all Google Cloud services.

* Predefined roles:
  * Roles that give finer-grained access control than the primitive roles. For example, the predefined role Pub/Sub Publisher (roles/pubsub.publisher) provides access to only publish messages to a Pub/Sub topic.

* Custom roles:
  * Roles that you create to tailor permissions to the needs of your organization when predefined roles don't meet your needs.

GKE IAM Predefined Roles - [here](https://cloud.google.com/kubernetes-engine/docs/how-to/iam#predefined)

GCE IAM Policies - [here](https://cloud.google.com/compute/docs/access#resource-policies)

## 1.4.c - Granting permissions to different types of identities

Cloud Identity and access management (IAM) is the practice of granting the right individuals access to the right resources for the right reasons. This series explores IAM and the individuals who are subject to it, including the following:

* **Corporate identities**: The identities that you manage for employees of your organization. These identities are used for signing in to workstations, accessing email, or using corporate applications. Corporate identities might also include non-employees such as contractors or partners that need access to corporate resources.
* **Customer identities**: The identities that you manage for users in order to interact with your website or customer-facing applications.
* **Service identities**: The identities that you manage in order to enable applications to interact with other applications or the underlying platform.

You might need to grant access to the following resources:
* Google services such as Google Cloud, Google Analytics, or G Suite
* Resources in Google Cloud, such as projects, Cloud Storage buckets, or virtual machines (VMs)
* Custom applications or resources managed by such applications

![IAM Descision Tree](images/IAM-Descision-Tree.png)

Customer identities are the identities that you manage for users to let them interact with your website or customer-facing applications. Managing customer identities and their access is also referred to as customer identity and access management (CIAM).

By leveraging Google's [Identity Platform](https://cloud.google.com/identity-platform) you can control access to your business application. It provides Single-Sign-On (SSO) capabilities. This is done by building [authentication workflows](https://cloud.google.com/identity-platform/docs/concepts-authentication) into the application using easy-to-use SDK functionality.


## 1.4.d - Understanding differences between Cloud Storage IAM and ACLs


Cloud Storage IAM and ACLs work in tandem to grant access to your buckets and objects: a user only needs permission from either Cloud IAM or an ACL to access a bucket or object.
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

## 1.4.e - Designing identity roles at the organization, folder, project, and resource level

![Cloud Identity Hierarchy](images/policy-hierarchy.png)

IAM lets you set policies at the following levels of the resource hierarchy:

* Organization level.
  * The organization resource represents your company. IAM roles granted at this level are inherited by all resources under the organization. For more information, see Access control for organizations using IAM.
    * Users who are not owners, including organization administrators, must be assigned either the `Organization Role Administrator role (roles/iam.organizationRoleAdmin)` or the IAM `Role Administrator role (roles/iam.roleAdmin)`. The IAM `Security Reviewer role (roles/iam.securityReviewer)` enables the ability to view custom roles but not administer them.

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

## 1.4.f - Configuring Access Context Manager

Access Context Manager allows enterprises to configure access levels which map to a policy defined on request attributes.

Access Context Manager allows Google Cloud organization administrators to define fine-grained, attribute based access control for projects and resources in Google Cloud.

Administrators first define an access policy, which is an organization-wide container for access levels and service perimeters.

Access levels describe the requirements for requests to be honored. Examples include:

* Device type and operating system
* IP address
* User identity

Service perimeters define sandboxes of resources which can freely exchange data within the perimeter, but are not allowed to export data outside of it. Access Context Manager isn't responsible for policy enforcement. Its purpose is to describe the desired rules. Policy is configured and enforced across various points, such as VPC Service Controls. You can read more about these services in their respective user guides.

Access Context Manager is an integral part of the BeyondCorp effort at Google. To learn more, see [BeyondCorp](https://cloud.google.com/beyondcorp/).

An access policy collects the service perimeters and access levels you create for your Organization. An Organization can only have one access policy.

When service perimeters are created and managed using the VPC Service Controls page of the Cloud Console, you do not need to create an access policy.

IAM Roles/Permissions required:

* accesscontextmanager.policies.create
  - Privided by the `roles/accesscontextmanager.policy.Editor` role
  - Allow creation of an orginzation-level access policy or scoped policies
* accesscontextmanager.policies.list
  - Provided by `roles/accesscontextmanager.policyEditor` and `roles/accesscontextmanager.policyReader`
  - Lists an organization-level access policy or scoped policies

```
gcloud access-context-manager policies create --organization ORGANIZATION_ID --title POLICY_TITLE
```

Set the default policy:

```
gcloud config set access-context-manager/policy POLICY_NAME
```

You can update the access levels of you organization by using the bulk management apis using gcloud cli:

```
gcloud access-context-manager levels replace-all \
  --source-file=FILE \
  --etag=ETAG \
  [--policy=POLICY_NAME]
```
 `FILE` is a `.yaml` file containing the access levels as shown below:

 ```
- name: accessPolicies/11271009391/accessLevels/corpnet_access
  title: Corpnet Access
  description: Permit access to corpnet.
  basic:
    combiningFunction: AND
    conditions:
      - ipSubnetworks:
        - 252.0.2.0/24
        - 2001:db8::/32
- name: accessPolicies/11271009391/accessLevels/prodnet_access
  title: Prodnet Access
  description: Permit access to prodnet.
  basic:
    combiningFunction: OR
    conditions:
      - members:
        - user:exampleuser@example.com
        - serviceAccount:exampleaccount@example.iam.gserviceaccount.com
      - ipSubnetworks:
        - 176.0.2.0/24
 ```


</details>

<details>
<summary> 1.5 Defining resource hierarchy </summary>

## 1.5.a - Creating and managing organizations

The following diagram outlines a sample resource outline in Google Cloud:
![Resource Outline](images/cloud-folders-hierarchy.png).

The _Origanization_ node is the root node and all access control policies will trickle down from there. Org admins can set [Originizational Policies](https://cloud.google.com/resource-manager/docs/organization-policy/overview), which is a configuration of restrictions that enforce access control to folders and/or projects at the organization level on resources and their descendants. Available for Google Workspace and Cloud Identity.

Best Practices For Organizations can be found [here](https://cloud.google.com/docs/enterprise/best-practices-for-enterprise-organizations).

Key Best Practices:
* Map you organizational structure to GCP
  * Top level node represents you organization, folders represent business units/departments and/or teams.
* Federate your identity provider with Google Cloud
  * If your organization uses an on-premises or third-party identity provider, synchronize your user directory with Cloud Identity to let users access Google Cloud with their corporate credentials. This way, your identity platform remains the source of truth while Cloud Identity controls how your employees access Google services.

The following image outlines an example of  resource hierarchy illustrating the core account-level resources involved in administering your Google Cloud account.
![Resource Hierarchy](images/resource-hierarchy-overview.png)

  * Billing Account:
    * Tracks all organizational costs
      * Linked to one or more projects
  * Domain:
    * Your company Domain is the primary identity of your organization and establishes your company's identity with Google services, including Google Cloud.
    * Can be linked to either a Google Workspace or Cloud Identity Account
  * Organization:
    * Highest unit
    * Using [Google Workspace](https://support.google.com/a/answer/53926) and [Cloud Identity](https://cloud.google.com/identity/docs/set-up-cloud-identity-admin)
    * Common place to register your domain as the org name
  * Folders:
    * Orgs can have one or more folders
    * Common practice is to have folders represent departments in an org
    * Max 100 folders under a single folder or up to four levels of folder nesting
  * Projects:
    * A unit of resource management below folders
    * Contains a set of users, API permission, billing information and provisioned resources
    * Identified by their project ID which should be unique to each org
    * Auto-generated by GCP
      * Cloud Deployment Manager
      * Terraform
      * Ansible
    * Users and roles specified per project
  * Resources:
    * Instance of offerings and services of GCP
    * Roles are defined to manage access

The following diagram outlines a sample resource outline in Google Cloud:
![Resource Outline](images/cloud-folders-hierarchy.png).

When you set an organization policy on a resource hierarchy node, all descendants of that resource hierarchy node inherit the organization policy by default. If you set an organization policy at the root organization node, then those restrictions are inherited by all child folders, projects, and resources.
You can set custom organization policy on child nodes, which will overwrite or merge with the inherited policy based on the rules of hierarchy evaluation.

**Resource Management Best Practices**

1. Mirror your Google Cloud resource hierarchy structure to your organization structure. The Google Cloud resource hierarchy should reflect how your company is organized, whether it's a startup, a SME, or a large corporation. A startup may start out with a flat resource hierarchy with no organization resource. When more people start collaborating on projects and the number of projects increase, getting an organization resource might make sense. An organization resource is recommended for larger companies with multiple departments and teams where each team is responsible for their own set of applications and services.

2. Use projects to group resources that share the same trust boundary. For example, resources for the same product or microservice can belong to the same project.

3. Set policies at the organization level and at the project level rather than at the resource level. As new resources are added, you may want them to automatically inherit policies from their parent resource. For example, as new virtual machines are added to the project through auto-scaling, they automatically inherit the policy on the project.



## 1.5.b - Designing resource policies for organizations, folders, projects, and resources

Organization policies are a set of configurations outlining restrictions associated to the organization, which can be assigned to organizations, folders, and projects in order to enforce the restrictions on that resource and its descendants.

![Org Policy](images/org-policy-concepts.svg)

In order to define an organization policy, you choose a [constraint](https://cloud.google.com/resource-manager/docs/organization-policy/overview#constraints), which is a particular type of restriction against either a Google Cloud service or a group of Google Cloud services. You configure that constraint with your desired restrictions.




## 1.5.c - Managing organization constraints

The Organization Policy Service gives you centralized and programmatic control over your organization's cloud resources. As the organization policy administrator, you will be able to configure constraints across your entire resource hierarchy.

Benefits
* Centralize control to configure restrictions on how your organization’s resources can be used.
* Define and establish guardrails for your development teams to stay within compliance boundaries.
* Help project owners and their teams move quickly without worry of breaking compliance.

Identity and Access Management focuses on **who**, and lets the administrator authorize who can take action on specific resources based on permissions.

Organization Policy focuses on **what**, and lets the administrator set restrictions on specific resources to determine how they can be configured.

## Policies and Constraints
The IAM role `roles/orgpolicy.policyAdmin` enables an administrator to manage organization policies.


### Constraints

A **_constraint_** is a definition of the behaviors that are controlled by an organization policy. A constraint has a type, either list or boolean.



### Restricting by domains
_Resource Manager_ provides a domain restriction constraint, used within an org policy, that can limit resource sharing based on domain.

Organization policies can use this constraint to limit resource sharing to a specified set of one or more G Suite domains, and exceptions can be granted on a per-folder or per-project basis.

The domain restriction constraint is not retroactive. Once a domain restriction is set, this limitation will apply to IAM policy changes made from that point forward, and not to any previous changes.

_For example, consider two related organizations: examplepetstore.com and altostrat.com. You have granted an examplepetstore.com identity an IAM role in altostrat.com. Later, you decided to restrict identities by domain, and implemented an organization policy with the domain restriction constraint in altostrat.com. In this case, the existing examplepetstore.com identities would not lose access in altostrat.com. From that point, you could only grant IAM roles to identities from the altostrat.com domain._

The domain restriction constraint is based on the `iam.allowedPolicyMemberDomains` list constraint.


## Resource Liens

The project resource is the base-level organizing entity. Organizations and folders may contain multiple projects. A project is required to use Google Cloud, and forms the basis for creating, enabling, and using all Google Cloud services, managing APIs, enabling billing, adding and removing collaborators, and managing permissions.
https://cloud.google.com/resource-manager/docs/access-control-proj
You can place a lien upon a project to block the project's deletion until you remove the lien. This can be useful to protect projects of particular importance.
```
gcloud alpha resource-manager liens create
  --restrictions=resourcemanager.projects.delete
  --reason="Super important production system"
```

## 1.5.d - Using resource hierarchy for access and permissions inheritance

The resource nodes that are in the hierarchy below the Organization Node are evaluated as follows:

* Resource 1 defines a custom policy that sets inheritFromParent to TRUE and allows blue diamond. The policy from the Organization Node is inherited and merged with the custom policy, and the effective policy evaluates to allow red square, green circle, and blue diamond.

* Resource 2 defines a custom policy that sets inheritFromParent to TRUE and denies green circle. Deny values always take precedence during policy reconciliation. The policy from the Organization Node is inherited and merged with the custom policy, and the effective policy evaluates to allow only red square.

* Resource 3 defines a custom policy that sets inheritFromParent to FALSE and allows yellow hexagon. The policy from the Organization Node is not inherited, so the effective policy evaluates to only allow yellow hexagon.

* Resource 4 defines a custom policy that sets inheritFromParent to FALSE and includes the restoreDefault value. The policy from the Organization Node is not inherited, and the default constraint behavior is used, so the effective policy evaluates to allow all.

Descendants of the targeted resource hierarchy node inherit the organization policy. By applying an organization policy to the root organization node, you are able to effectively drive enforcement of that organization policy and configuration of restrictions across your organization.

You can set an Cloud IAM policy at the organization level, the folder level, the project level, or (in some cases) the resource level. Resources inherit the policies of the parent node. If you set a policy at the Organization level, it is inherited by all its child folders and projects, and if you set a policy at the project level, it is inherited by all its child resources.

The effective policy for a resource is the union of the policy set on the resource and the policy inherited from its ancestors. This inheritance is transitive. In other words, resources inherit policies from the project, which inherit policies from the organization. Therefore, the organization-level policies also apply at the resource level.

Ex:
![Org Policy Inheritance](images/org-policy-inheritance.svg)

## 1.5.e - Designing and managing trust and security boundaries within Google Cloud

**VPC Service Controls**

VPC Service Controls improves your ability to mitigate the risk of data exfiltration from Google Cloud services such as Cloud Storage and BigQuery. With VPC Service Controls, you create perimeters that protect the resources and data of services that you explicitly specify.

To configure Service Control perimeter:
1. If you want to use the gcloud command-line tool or the Access Context Manager APIs to create your service perimeters, create an access policy.

_**Note**: You do not have to manually create an access policy if you are using the Cloud Console to manage VPC Service Controls. An access policy will be created for your Organization automatically.
Secure GCP resources with service perimeters._

2. Set up VPC accessible services to add additional restrictions to how services can be used inside your perimeters (optional).
3. Set up private connectivity from a VPC network (optional).
4. Grant access from outside a service perimeter using access levels (optional).
5. Set up resource sharing between perimeters using service perimeter bridges (optional).

[Creating a service perimeter](https://cloud.google.com/vpc-service-controls/docs/create-service-perimeters)

</details>

# Section 2. Configuring network security
<details>
<summary> 2.1 Designing network security</summary>

## VPCs
Best practices for secure VPC designs can be found [here](https://cloud.google.com/solutions/best-practices-vpc-design).

A Virtual Private Cloud (VPC) network is a virtual version of a physical network, implemented inside of Google's production network, using [Andromeda](https://www.usenix.org/system/files/conference/nsdi18/nsdi18-dalton.pdf). A VPC network provides the following:

* Provides connectivity for your Compute Engine virtual machine (VM) instances, including Google Kubernetes Engine (GKE) clusters, App Engine flexible environment instances, and other Google Cloud products built on Compute Engine VMs.
* Offers native Internal TCP/UDP Load Balancing and proxy systems for Internal HTTP(S) Load Balancing.
* Connects to on-premises networks using Cloud VPN tunnels and Cloud Interconnect attachments.
* Distributes traffic from Google Cloud external load balancers to backends.

VPC networks have the following properties:
* VPC networks, including their associated routes and firewall rules, are global resources. They are not associated with any particular region or zone.
* Subnets are regional resources. Each subnet defines a range of IP addresses.
* Traffic to and from instances can be controlled with network firewall rules.
* Resources within a VPC network can communicate with one another by using internal (private) IPv4 addresses, subject to applicable network firewall rules. [See Network Communication](https://cloud.google.com/vpc/docs/vpc#intra_vpc_reqs)
* Instances with internal IP addresses can communicate with Google APIs and services. [See Private Access](https://cloud.google.com/vpc/docs/private-access-options)
* Network administration can be secured by using Cloud Identity and Access Management (Cloud IAM) roles.
* An organization can use Shared VPC to keep a VPC network in a common host project. Authorized Cloud IAM members from other projects in the same organization can create resources that use subnets of the Shared VPC network.
* VPC networks can be connected to other VPC networks in different projects or organizations by using VPC Network Peering.
* VPC networks can be securely connected in hybrid environments by using Cloud VPN or Cloud Interconnect.
* VPC networks only support IPv4 unicast traffic. They do not support broadcast, multicast, or IPv6 traffic within the network; VMs in the VPC network can only send to IPv4 destinations and only receive traffic from IPv4 sources. However, it is possible to create an IPv6 address for a global load balancer.

Each VPC network consists of one or more useful IP range partitions called _subnets_. Each subnet is associated with a region. VPC networks do not have any IP address ranges associated with them. IP ranges are defined for the subnets.

A network must have at least one subnet before you can use it. Auto mode VPC networks create subnets in each region automatically. Custom mode VPC networks start with no subnets, giving you full control over subnet creation. You can create more than one subnet per region.

Types of networks:
* **_auto mode_**:
  * one subnet from each region is automatically created within it. These automatically created subnets use a set of predefined IP ranges that fit within the 10.128.0.0/9 CIDR block. As new Google Cloud regions become available, new subnets in those regions are automatically added to auto mode VPC networks by using an IP range from that block.In addition to the automatically created subnets, you can add more subnets manually to auto mode VPC networks in regions that you choose by using IP ranges outside of 10.128.0.0/9.
* **_custom mode_**:
  * no subnets are automatically created. This type of network provides you with complete control over its subnets and IP ranges. You decide which subnets to create in regions that you choose by using IP ranges that you specify.

You can switch from auto mode to custom mode but this is a one-way conversion.

Considerations for choosing _auto mode_ over _custom mode_:

* auto mode:
  * easy to set up and use
  * Having subnets automatically created in each region is useful.
  * The predefined IP ranges of the subnets do not overlap with IP ranges that you would use for different purposes (for example, Cloud VPN connections to on-premises resources).

* custom mode:
  * more flexible and bettwe suited for production use cases
  * Having one subnet automatically created in each region isn't necessary.
  * Having new subnets automatically created as new regions become available could overlap with IP addresses used by manually created subnets or static routes, or could interfere with your overall network planning.
  * You need complete control over the subnets created in your VPC network, including regions and IP address ranges used.
  * You plan to connect VPC networks by using VPC Network Peering or Cloud VPN. Because the subnets of every auto mode VPC network use the same predefined range of IP addresses, you cannot connect auto mode VPC networks to one another.

**Default network**
Unless you choose to disable it, each new project starts with a default network. The default network is an auto mode VPC network with pre-populated firewall rules.

You can disable the creation of default networks by creating an organization policy with the `compute.skipDefaultNetworkCreation` constraint. Projects that inherit this policy won't have a default network.

### Subnets
When you create a subnet, you must define its primary IP address range. The primary internal addresses for the following resources come from the subnet's primary range: VM instances, internal load balancers, and internal protocol forwarding. You can optionally add secondary IP address ranges to a subnet, which are only used by alias IP ranges. However, you can configure alias IP ranges for instances from the primary or secondary range of a subnet.

The following table shows valid CIDR block ranges for IPs
![Valid IP Ranges](images/valid-subnet-ranges.png)

Every subnet has four reserved IP addresses in its primary IP range. There are no reserved IP addresses in the secondary IP ranges.
![reserved ips](images/reserved_ip_ranges.png)

**Code Snippets**
To Create a vpc you can leverage the following GCloud CLI command:
```
gcloud compute networks create NETWORK_NAME
    --subnet-mode=auto|custom
    --bgp-routing-mode=global|regional
```

To add a _subnet_ to an existing VPC use the following command:
```
gcloud compute networks subnets create SUBNET_NAME \
    --network=NETWORK \
    --range=PRIMARY_RANGE \
    --region=REGION
```

You can modify the previous command with the following optional flags:

* `--secondary-range=SECONDARY_RANGE_NAME=SECONDARY_RANGE`:
  * Replace SECONDARY_RANGE_NAME with name for the secondary range and SECONDARY_RANGE with a secondary range in CIDR notation. The per network limits describe the maximum number of secondary ranges that you can define for each subnet.
* `--enable-flow-logs`:
  *  Enables VPC Flow Logs in the subnet at creation time.
* `--enable-private-ip-google-access`:
  * Enables Private Google Access in the subnet at creation time.
* `--purpose`:
  * Enables you to creata a proxy-only subnet for Internal HTTP(S) Load Balancing.
* `--role`:
  * For Internal HTTP(S) Load Balancing only, enables you to specify a role (active or backup) for a proxy-only subnet.

To delete a subnet use the following command:
```
gcloud compute networks subnets delete SUBNET_NAME \
    --region=REGION
```

To delete a VPC use the following command:
```
gcloud compute networks delete NETWORK_NAME
```

[Multi VPC Lab](https://www.qwiklabs.com/focuses/1230?parent=catalog)

### Network Intelligence Center

Network Intelligence Center provides a single console for managing Google Cloud network visibility, monitoring, and troubleshooting.

**Network Topology**
Visualize the topology of your VPC network and their associated metrics

**Connectivity Tests**
Test network connectivity to and from your VPC

**Performance Dashboard**
View packet loss and latency between zones where have VMs

**Firewall Insights**
View usage for your VPC firewall rules and optimize thier configuration

**Network Analyzer**
View network and service issues, insights, and best practice recommendations from automatic discoveries


## 2.1.a Configuring network perimeter controls

**VPC Service Controls**

VPC Service Controls improves your ability to mitigate the risk of data exfiltration from Google Cloud services such as Cloud Storage and BigQuery. With VPC Service Controls, you create perimeters that protect the resources and data of services that you explicitly specify.

To configure Service Control perimeter:
1. If you want to use the gcloud command-line tool or the Access Context Manager APIs to create your service perimeters, create an access policy.

_**Note**: You do not have to manually create an access policy if you are using the Cloud Console to manage VPC Service Controls. An access policy will be created for your Organization automatically.
Secure GCP resources with service perimeters._

2. Set up VPC accessible services to add additional restrictions to how services can be used inside your perimeters (optional).
3. Set up private connectivity from a VPC network (optional).
4. Grant access from outside a service perimeter using access levels (optional).
5. Set up resource sharing between perimeters using service perimeter bridges (optional).

![VPC Service Controls](https://cloud.google.com/static/vpc-service-controls/images/service_perimeter.png)

You can configure private communication to Google Cloud resources from VPC networks that span hybrid environments with Private Google Access [on-premise extensions](https://cloud.google.com/vpc-service-controls/docs/private-connectivity). A VPC network must be part of a service perimeter for VMs on that network to privately access managed Google Cloud resources within that service perimeter.

VMs with private IPs on a VPC Network that is part of a service perimeter cannot access managed resources outside the service perimeter. If necessary, you can continue to enable inspected and audited access to all Google APIs (for example, Gmail) over the internet.

The following diagram shows a service perimeter that extends to hybrid environments with Private Google Access:

![hybrid VPC Service Controls](https://cloud.google.com/static/vpc-service-controls/images/service_perimeter_private.png)

You can leverage Service Perimeter bridges to allow projects in different service perimeters to communicate. Perimeter Bridges are bidirectional, allow projects form each service perimeter equal access within the scope of the bridge.

[Creating a service perimeter](https://cloud.google.com/vpc-service-controls/docs/create-service-perimeters)

**gcloud - create vpc service perimeter**

```
gcloud access-context-manager perimeters create NAME \
  --title=TITLE \
  --resources=PROJECTS \
  --restricted-services=RESTRICTED-SERVICES \
  --ingress-policies=INGRESS-FILENAME.yaml \
  --egress-policies=EGRESS-FILENAME.yaml \
  [--access-levels=LEVELS] \
  [--enable-vpc-accessible-services] \
  [--vpc-allowed-services=ACCESSIBLE-SERVICES] \
  --policy=POLICY_NAME
```

In dry-run mode:

```
gcloud access-context-manager perimeters dry-run create NAME \
  --perimeter-title=TITLE \
  --perimeter-type=TYPE \
  --perimeter-resources=PROJECTS \
  --perimeter-restricted-services=RESTRICTED-SERVICES \
  --perimeter-ingress-policies=INGRESS-FILENAME.yaml \
  --perimeter-egress-policies=EGRESS-FILENAME.yaml \
  [--perimeter-access-levels=LEVELS] \
  [--perimeter-enable-vpc-accessible-services] \
  [--perimeter-vpc-allowed-services=ACCESSIBLE-SERVICES] \
  --policy=POLICY_NAME
```

Sample:
```
gcloud access-context-manager perimeters \
  dry-run create ProdPerimeter --perimeter-title="Production Perimeter" \
  --perimeter-type="regular" \
  --perimeter-resources=projects/12345,projects/67890 \
  --perimeter-restricted-services=storage.googleapis.com,bigquery.googleapis.com \
  --perimeter-ingress-policies=ingress.yaml \
  --perimeter-egress-policies=egress.yaml \
  --policy=330193482019
```

## 2.1.b Configuring load balancing (global, network, HTTP(s), SSL Proxy, and TCP Proxy)

A load balancer distributes user traffic across multiple instances of your applications. By spreading the load, load balancing reduces the risk that your applications become overburdened, slow, or nonfunctional.

![LB](images/lb-simple-overview.svg)

Types of LBs:
* Global LB
  * Use this when you backen servers are across multi regions / continents
  * Provides IPV6 termination
* Regional LB
  * Use when only One region is needed
  * Use when only IPV 4 termination is needed
* External load balancers:
  * distribute traffic coming from the internet to your Google Cloud Virtual Private Cloud (VPC) network. They are Global load balancing requires that you use the Premium Tier of Network Service Tiers. For regional load balancing, you can use Standard Tier.
* Internal load balancers distribute traffic to instances inside of Google Cloud network permimeter.

GC Cloud Load balancers is built on the following propretary products:

* [Google Front End(s)](https://cloud.google.com/security/infrastructure/design#google_front_end_service):
  * which are software-defined, distributed systems that are located in Google points of presence (PoPs) and perform global load balancing in conjunction with other systems and control planes.
  * smart reverse-proxy
  * provides public IP hosting of its public DNS name, Denial of Service (DoS) protection, and TLS termination
* [Andromeda](https://cloudplatform.googleblog.com/2014/04/enter-andromeda-zone-google-cloud-platforms-latest-networking-stack.html)
  * is Google Cloud's software-defined network virtualization stack.
* [Maglev](https://research.google/pubs/pub44824/)
  * which is a distributed system for Network Load Balancing
* [Envoy proxy](https://www.envoyproxy.io/)
  * is an open source edge and service proxy, designed for cloud-native applications

* Internal HTTP(s) Load Balancer (Layer 7)
  * built on the Andromeda network virtualization stack and is a managed service based on the open source Envoy proxy. This load balancer provides proxy-based load balancing of Layer 7 application data. You specify how traffic is routed with URL maps. The load balancer uses an internal IP address that acts as the frontend to your backends.
* External HTTP(s) Load Balancer (Layer 7)
  * is implemented on GFEs. GFEs are distributed globally and operate together using Google's global network and control plane. In Premium Tier, GFEs offer cross-regional load balancing, directing traffic to the closest healthy backend that has capacity and terminating HTTP(S) traffic as close as possible to your users.
  * Provides DDoS Protection
* Internal TCP/UDP Load Balancer (Layer 4)
  * is built on the Andromeda network virtualization stack. Internal TCP/UDP Load Balancing enables you to load balance TCP/UDP traffic behind an internal load balancing IP address that is accessible only to your internal virtual machine (VM) instances. By using Internal TCP/UDP Load Balancing, an internal load balancing IP address is configured to act as the frontend to your internal backend instances. You use only internal IP addresses for your load balanced service. Overall, your configuration becomes simpler.
  * supports regional managed instance groups so that you can autoscale across a region, protecting your service from zonal failures.
* External TCP/UDP Load Balancer (Layer 4)
  * Built on Maglev.
  * enables you to load balance traffic on your systems based on incoming IP protocol data, including address, port, and protocol type. It is a regional, non-proxied load balancing system. Use Network Load Balancing for UDP traffic, and for TCP and SSL traffic on ports that are not supported by the SSL proxy load balancer and TCP proxy load balancer. A network load balancer is a pass-through load balancer that does not proxy connections from clients.
* SSL Proxy Load Balancer (Layer 7)
  * intended for non-HTTP(S) traffic
  * Implemented on GFEs that are distributed globally.
  * If you choose the Premium Tier of Network Service Tiers, an SSL proxy load balancer is global. In Premium Tier, you can deploy backends in multiple regions, and the load balancer automatically directs user traffic to the closest region that has capacity. If you choose the Standard Tier, an SSL proxy load balancer can only direct traffic among backends in a single region.
  * Provides DDoS protection
  * Supports load balancing on ports:
    * 25, 43, 110, 143, 195, 443, 465, 587, 700, 993, 995, 1883, 3389, 5222, 5432, 5671, 5672, 5900, 5901, 6379, 8085, 8099, 9092, 9200, and 9300.
* TCP Proxy Load Balancer (Layer 4)
  * implemented on GFEs that are distributed globally. If you choose the Premium Tier of Network Service Tiers, a TCP proxy load balancer is global. In Premium Tier, you can deploy backends in multiple regions, and the load balancer automatically directs user traffic to the closest region that has capacity. If you choose the Standard Tier, a TCP proxy load balancer can only direct traffic among backends in a single region.

Choosing the right Cloud Load Balancer:
![Choose LB](images/choose-lb.svg)

[HTTP Load Balancer Lab](https://google.qwiklabs.com/focuses/12007?parent=catalog)
[Internal Load Balancer Lab](https://google.qwiklabs.com/focuses/1250?catalog_rank=%7B%22rank%22%3A1%2C%22num_filters%22%3A0%2C%22has_search%22%3Atrue%7D&parent=catalog&search_id=6735196)

#### SSL Policies
SSL policies give you the ability to control the features of SSL that your Google Cloud SSL proxy load balancer or external HTTP(S) load balancer negotiates with clients.

By default, HTTP(S) Load Balancing and SSL Proxy Load Balancing use a set of SSL features that provides good security and wide compatibility. Some applications require more control over which SSL versions and ciphers are used for their HTTPS or SSL connections. You can define SSL policies to control the features of SSL that your load balancer negotiates with clients.

The TLS versions currently supported are TLS 1.0, 1.1 and 1.2. SSL 3.0 or ealier is no longer supported by GCP Load Balancers or SSL Proxy.

There are three pre-configured Google Managed profiles which allow you to specify the level of compatibility that is appropriate for your application. A custom profile is also provided that allows you to select the SSL features you want manually.

Google Managed Profiles:
* **COMPATIBLE**:
  * allows the broadset of clients, including those which support only out-of-date SSL features, to negotiate SSL with the LB.
* **MODERN**:
  * Supports a wide set of SSL features, allowing modern clients to negotiate SSL.
* **RESTRICTED**:
  * Supports a reduced set of SSL features, intended to meet stricter compliance requirements.

The following table shows all the current features available that are supported for each pre-configured profiles.

![SSL Policy Features](images/SSL-Policy-Features-Pre-Configured.png)

**NOTE: If no SSL Policy is specified, the default SSL Policy is used which is equivalent to the _COMPATIBLE_ profile.**

Enabling an SSL Policy using `glcoud` command line tool:

```
gcloud compute ssl-policies create NAME \
    --profile COMPATIBLE|MODERN|RESTRICTED|CUSTOM \
    --global \
    [--min-tls-version 1.0|1.1|1.2] \
    [--custom-features FEATURES]
```

Using pre-defined (MODERN):
```
gcloud compute ssl-policies create my_ssl_policy \
    --global \
    --profile MODERN    \
    --min-tls-version 1.0
```

Using custom profile:
```
gcloud compute ssl-policies create NAME \
    --global \
    --profile CUSTOM --min-tls-version 1.2 \
    --custom-features "TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305_SHA256,"\
    "TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305_SHA256"
```

You can also create a target SSL Proxy or HTTPS proxy with an SSL Policy:

```
gcloud compute target-ssl-proxies | target-https-porxy create NAME \
    --backend-service BACKEND_SERVICE_NAME \
    --ssl-certificate SSL_CERTIFICATE_NAME \
    [--ssl-policy SSL_POLICY_NAME]
```

### Network Endpoint Groups (NEGs)
A _network endpoint group (NEG)_ is a configuration object which specifies a group of backend endpoints/services. HTTPs (internal/external) LBs and External SSL/TCP proxy LBs support NEGs.

NEG Types:

* Zonal
  * Contains one or more endpoints that can be compute engine vms or services running on the VMs.
  * Endpoints are specified via `IP:port` combo
* Internet
  * contains a single endpoint that is hosted outside of GCP.
  * specified by a hostname `FQDN:port` or `IP:port` combination.
* Serverless
  * points to Cloud Run, App Engine or Cloud Functions services
  * MUST reside in the same region

**Standalone NEGs**
When NEGs are deployed with load balancers provisioned by anything other than Ingress, they are considered standalone NEGs. Standalone NEGs are deployed and managed through the NEG controller, but the forwarding rules, health checks, and other load balancing objects are deployed manually.

Standalone NEGs do not conflict with Ingress enabled container-native load balancing.

The following diagram shows the difference in how the load balancing objects are deployed in each scenerio:
![Standalone NEGs](images/sneg2.svg)


## 2.1.c Configuring Cloud DNS & Identifying Domain Name System Security Extensions (DNSSEC)

## Cloud DNS & DNS Security
Cloud DNS is a high-performance, resilient, global Domain Name System (DNS) service that publishes your domain names to the global DNS in a cost-effective way.

DNS is a hierarchical distributed database that lets you store IP addresses and other data, and look them up by name. Cloud DNS lets you publish your zones and records in DNS without the burden of managing your own DNS servers and software.

Cloud DNS offers both public zones and private managed DNS zones. A public zone is visible to the public internet, while a private zone is visible only from one or more Virtual Private Cloud (VPC) networks that you specify. For detailed information about zones, see DNS zones overview.

Cloud DNS supports Identity and Access Management (IAM) permissions at the project level and individual DNS zone level (available in Preview). For information about how to set individual resource IAM permissions, see Create a zone with specific IAM permissions.

DNSSEC is a feature of the Domain Name System that authenticates responses to domain name lookups. It does not provide privacy protections for those lookups, but prevents attackers from manipulating or poisoning the responses to DNS requests.

**Terminalogy**
* Managed Zones:
  * This zone holds DNS records for the same DNS name suffix (example.com, for example). There can be multiple managed zones in a GCP project, but they all must have a unique name.In Cloud DNS, the managed zone is the resource that models a [DNS zone](https://en.wikipedia.org/wiki/DNS_zone). All records in a managed zone are hosted on the same Google-operated name servers. These name servers respond to DNS queries against your managed zone according to how you configure the zone.
* Public Zones:
  * This zone is visible to the internet. Cloud DNS has public authoritative name servers that respond to queries about public zones regardless of where the queries originate. You can create DNS records in a public zone to publish your service on the internet. For example, you might create the following record in a public zone example.com for your public web site www.example.com.

ex:
|DNS Name|Type|TTL (seconds)|Data|
|--------|-----|------------|-------|
|www.example.com|	A|	300	198.51.100.0|

Cloud DNS assigns a set of name servers when a public zone is created. For the DNS records in a public zone to be resolvable over the internet, you must update the name server setting of your domain registration at your registrar.

* Private Zones:
  * This zone enables you to manage custom domain names for your virtual machines, load balancers, and other Google Cloud resources without exposing the underlying DNS data to the public internet. A private zone is a container of DNS records that can only be queried by one or more VPC networks that you authorize. A private zone can only be queried by resources in the same project where it is defined. The VPC networks that you authorize must be located in the same project as the private zone. If you need to query records hosted in managed private zones in other projects, use DNS peering.
  * Private zones do not support DNS security extensions (DNSSEC) or custom resource record sets of type NS.

ex:
|DNS Name|	Type|	TTL (seconds)|	Data|
|--------|------|----------------|------|
|db-01.dev.gcp.example.com|	A|	5|	10.128.1.35|
|instance-01.dev.gcp.example.com|	A|	50|	10.128.1.10|
* Forwarding Zones:
  * A forwarding zone is a type of Cloud DNS managed private zone that sends requests for that zone to the IP addresses of its forwarding targets. For more information, see DNS forwarding methods.

For more details on Cloud DNS go [here](https://cloud.google.com/dns/docs/overview)

## Domain Name System Security Extensions (DNSSEC)
DNSSEC is a feature of the Domain Name System that authenticates responses to domain name lookups. It does not provide privacy protections for those lookups, but prevents attackers from manipulating or poisoning the responses to DNS requests.

DNSSec needs to be enabled in the following three places:

1. The DNS zone for your domain must serve special DNSSEC records for public keys (DNSKEY), signatures (RRSIG), and non-existence (NSEC, or NSEC3 and NSEC3PARAM) to authenticate your zone's contents. Cloud DNS manages this automatically if you [enable DNSSEC for a zone](https://cloud.google.com/dns/docs/dnssec-config#enabling).
2. The top-level domain registry (for example.com, this would be .COM) must have a DS record that authenticates a DNSKEY record in your zone. Do this by activating DNSSEC at your domain registrar.
3. For full DNSSEC protection, clients must use a DNS resolver that validates signatures for DNSSEC-signed domains. You can enable validation for individual systems or the local DNS resolvers (Refer to the appendices in this PDF guide). You can also configure systems to use public resolvers that validate DNSSEC, notably Google Public DNS and Verisign Public DNS.

You can enable DNSSec on Cloud DNS managed zones through the console window as well as throught the Command Line Interface(CLI) tools:

On an existing managed zone:
```
gcloud dns managed-zones update EXAMPLE_ZONE --dnssec-state on
```

At creation time:
```
gcloud dns managed-zones create EXAMPLE_ZONE \
    --description "Signed Zone" --dns-name myzone.example.com --dnssec-state on
```

### Verifying DNSSEC deployment
You can use [DNSViz](http://dnsviz.net/),the [Verisign DNSSEC debugger](http://dnssec-debugger.verisignlabs.com/), or [Zonemaster](https://zonemaster.net/) to verify correct deployment of your DNSSEC-enabled zone (the latter two can also be used before you update your registrar with your Cloud DNS name servers or DS record to activate DNSSEC). An example of a domain that is properly configured for DNSSEC is example.com; you can see it with DNSViz at http://dnsviz.net/d/www.example.com/dnssec/.

### dig Command
The `dig` command allows you to query information about various DNS records, including host addressses, mail exchanges, and name servers. Its mostly used by system adminstrators for troubleshooting DNS problems.

`$ dig linux.org`

Output:

![dig output](https://linuxize.com/post/how-to-use-dig-command-to-query-dns-in-linux/dig-command-output_hu636dcbe221bee3ee0ad79218c8886c5f_81144_768x0_resize_q75_lanczos.jpg)

More details on the dig command can be found [here](https://linuxize.com/post/how-to-use-dig-command-to-query-dns-in-linux/)

## 2.1.d Identifying differences between private versus public addressing

## 2.1.e Configuring web application firewall (Cloud Armour)


Google Cloud Armour helps you protect your Google Cloud deployments from multiple types of threats, including distributed denial-of-service (DDoS) attacks and application attacks like cross-site scripting (XSS) and SQL injection (SQLi). Google Cloud Armor features some automatic protections and some that you need to configure manually. This document provides a high-level overview of these features, several of which are only available for global external HTTP(S) load balancers and global external HTTP(S) load balancer (classic)s.

### Security Policies

External load balancing is implemented at the edge of Google's network in their [Points of presence (PoPs)](https://cloud.google.com/about/locations#network-tab), Cloud Armour security policies apply at the edge for external load balancers:

![GLB Cloud Armour](https://cloud.google.com/static/armor/images/ip-bl-wl-1.svg)

* Protect applications running behind a load balancer from DDoS and other web attacks
* Configured manually with configurable match conditions and actions
* Comes with preconfigured policies that cover a variety of uses Cases
* Backend Services can be either of the following:
  - [Instances Groups](https://cloud.google.com/compute/docs/instance-groups)
  - [Zonal network endpoint groups (NEGs)](https://cloud.google.com/load-balancing/docs/negs/zonal-neg-concepts)
  - [Serverless NEGs](https://cloud.google.com/load-balancing/docs/negs/serverless-neg-concepts)
    - One or more AppEngine, Cloud Run or Cloud Functions Services
  - [Internet NEGs](https://cloud.google.com/load-balancing/docs/negs/internet-neg-concepts)
  - Cloud Storage buckets
* Provides advanced network DDoS protection for External TCP/UDP network load balancer, protocol forwarding, and VMs with public IP addresses (BETA FEATURE)
  - https://cloud.google.com/armor/docs/advanced-network-ddos


Common uses cases : https://cloud.google.com/armor/docs/common-use-cases

#### Configuring Security Policies
You require either the `roles/compute.securityAdmin` or `roles/compute.networkAdmin` in order to configure Cloud Armour Security Policies:
* SecurityAdmin can access the following APIs:
  - SecurityPolicies insert, delete, patch, addRule, patchRule, removeRule
  - Configuring, modifying, updating, and deleting of Cloud Armour Security Policies
* NetworkAdmin can setup a Cloud Armour security policy for a backend service
  - Access the following APIs:
    - BackendServices setSecurityPolicy, list (gcloud only)

**Create the Cloud Armour Security policies**
```
gcloud compute security-policies create mobile-clients-policy \
    --description "policy for external users"
```

Update the default rules to deny traffic:

```
gcloud compute security-policies rules update 2147483647 \
    --security-policy mobile-clients-policy \
    --action "deny-404"
```

You can use the `--enable-layer7-ddos-defense` flag to enable [Adpative Protection](https://cloud.google.com/armor/docs/adaptive-protection-overview)

Add Rules
```
gcloud compute security-policies rules create 1000 \
    --security-policy mobile-clients-policy \
    --description "allow traffic from 192.0.2.0/24" \
    --src-ip-ranges "192.0.2.0/24" \
    --action "allow"
```

Attach the policy to a the backend service:
```
gcloud compute backend-services update games \
    --security-policy mobile-clients-policy
```

`--type=CLOUD_ARMOUR|CLOUD_ARMOUR_EDGE` is optional and defaults to `CLOUD_ARMOUR` which creates a backend security policy. `CLOUD_ARMOUR_EDGE` is used to create an edge policy.

**GKE BackendConfig**

Once a security policy is configured your can reference it using the `BackendConfig` kubernetes config.

```
apiVersion: cloud.google.com/v1
kind: BackendConfig
metadata:
  namespace: cloud-armor-how-to
  name: my-backendconfig
spec:
  securityPolicy:
    name: "example-security-policy"
```


### Rules language
Cloud Armour custom rules language is an extension of the [Common Expression Language (CEL)](https://github.com/google/cel-spec) and is used to write expressions for advanced matching conditions for security policy rules.

An expression requires two components:
  * _Attributes_ that can be inspected in rule expressions.
  * _Operations_ that can be performed on the attributes as part of an expression.

The following example creates a rate-based ban rule at priority 100 for each IP address whose requests match a header `fist` with value `tuna` and ban it for 300 seconds when its rate exceeds a limit of 50 requests for each 120 seconds. Banned requests return an error code of `404`.

```
gcloud compute security-policies rules create 100 \
    --security-policy=sec-policy     \
    --expression="request.headers['fish'] == 'tuna'" \
    --action=rate-based-ban                   \
    --rate-limit-threshold-count=50           \
    --rate-limit-threshold-interval-sec=120   \
    --ban-duration-sec=300           \
    --conform-action=allow           \
    --exceed-action=deny-404         \
    --enforce-on-key=IP
```

The following example creates a rate-based ban rule at priority 101 to limit all requests whose region code matches `US` to 10 requests per 60 seconds. The rule also bans requests from the US region for 300 seconds when their rate exceeds a limit of 1000 requests per 600 seconds. Banned requests return an error code of `403`.

```
gcloud compute security-policies rules create 101 \
   --security-policy sec-policy     \
   --expression "origin.region_code == 'US'" \
   --action rate-based-ban                 \
   --rate-limit-threshold-count 10         \
   --rate-limit-threshold-interval-sec 60  \
   --ban-duration-sec 300           \
   --ban-threshold-count 1000       \
   --ban-threshold-interval-sec 600 \
   --conform-action allow           \
   --exceed-action deny-403         \
   --enforce-on-key ALL
```

You can use `--src-ip-ranges="*"` to setup a rule that applies to any source IP address range.

You can also issue rate-based bans for users who have a valid reCAPTCHA exemption cookie. For example, the following gcloud command creates a throttle rule at priority 115 with a rate limit of 20 requests per 5 minutes for each unique reCAPTCHA exemption cookie across all requests that have a valid reCAPTCHA exemption cookie. Requests that exceed the throttling limit are redirected for reCAPTCHA Enterprise assessment. For more information about exemption cookies and reCAPTCHA Enterprise assessment, see the [bot management overview](https://cloud.google.com/armor/docs/bot-management)

```
gcloud compute security-policies rules create 115 \
    --security-policy sec-policy     \
    --expression="token.recaptcha_exemption.valid"     \
    --action=throttle                \
    --rate-limit-threshold-count=20 \
    --rate-limit-threshold-interval-sec=300 \
    --conform-action=allow           \
    --exceed-action=redirect         \
    --exceed-redirect-type=google-recaptcha         \
    --enforce-on-key=HTTP-COOKIE         \
    --enforce-on-key-name="recaptcha-ca-e"
```

Other sec policy examples can be found [here](https://cloud.google.com/armor/docs/configure-security-policies)

### Cloud Armour Tiers

|  Feature   | Cloud Armour Standard | Managed Protection Plus |
| ---------- | ----------------------| ----------------------- |
| Billing method | Pay-as-you-go   | Monthly subscription + Data Processing Fee (see [pricing](https://cloud.google.com/armor/pricing))   |
|DDoS Attack Protection   | HTTP(s) Load Balancing, External TCP / SSL Proxy Load Balancing   | HTTP(s) Load Balancing, External TCP / SSL Proxy Load Balancing   |
| WAF   | Per policy, per rule, pre request (see [Pricing](https://cloud.google.com/armor/pricing))   |  Included with Plus subscription |
| Resource Limits   | Up to quota limits   | Up to quota limits   |
|Preconfigured WAF rules   | Yes   | Yes   |
|Time commitment   | N/A  | One year   |
|Named IP address list   | No   | Yes   |
|Threat Intelligence (Preview)  | No   | Yes   |
|Adaptive Protection   | Alerting only   | Yes  |
|DDoS response support   | N/A   | Yes (w/ Premium support)   |
|DDoS bill protection   | N/A   | Yes   |


</details>

<details>
<summary>2.2 Configuring Network Segmentation</summary>

## 2.2.a - Configuring security properties of a VPC network, VPC peering, Shared VPC and firewall rules

### Firewalls
Virtual Private Cloud (VPC) firewall rules apply to a given project and network. You can create a [Firewall Policy](https://cloud.google.com/vpc/docs/firewall-policies) that can be applied accross the orgranization.

VPC firewall rules let you allow or deny connections to or from your virtual machine (VM) instances based on a configuration that you specify. Enabled VPC firewall rules are always enforced, protecting your instances regardless of their configuration and operating system, even if they have not started up.

Every VPC network functions as a distributed firewall. While firewall rules are defined at the network level, connections are allowed or denied on a per-instance basis. You can think of the VPC firewall rules as existing not only between your instances and other networks, but also between individual instances within the same network.

In addition to firewall rules that you create, Google Cloud has other rules that can affect incoming (ingress) or outgoing (egress) connections:

* Google Cloud doesn't allow certain IP protocols, such as egress traffic on TCP port 25 within a VPC network.
* Google Cloud always allows communication between a VM instance and its corresponding metadata server at 169.254.169.254.
* Every network has **two implied firewall rules that permit outgoing connections and block incoming connections**. Firewall rules that you create can override these implied rules. These implied rules apply to all instances in the network.
* The default network is pre-populated with firewall rules that you can delete or modify.

Firewall rules only support IPv4 connections.

To create a firewall rule use the following command:

```
gcloud compute firewall-rules create NAME \
    [--network NETWORK; default="default"] \
    [--priority PRIORITY;default=1000] \
    [--direction (ingress|egress|in|out); default="ingress"] \
    [--action (deny | allow )] \
    [--target-tags TAG,TAG,...] \
    [--target-service-accounts=IAM Service Account,IAM Service Account,...] \
    [--source-ranges CIDR-RANGE,CIDR-RANGE...] \
    [--source-tags TAG,TAG,...] \
    [--source-service-accounts=IAM Service Account,IAM Service Account,...] \
    [--destination-ranges CIDR-RANGE,CIDR-RANGE...] \
    [--rules (PROTOCOL[:PORT[-PORT]],[PROTOCOL[:PORT[-PORT]],...]] | all ) \
    [--disabled | --no-disabled] \
    [--enable-logging | --no-enable-logging] \
    [--logging-metadata logging_metadata]
```

Use the parameters as follows. More details about each are available in the SDK reference documentation.

* `--network`:
  * The network where the rule will be created. If omitted, the rule will be created in the default network. If you don't have a default network or want to create the rule in a specific network, you must use this field.
* `--priority`:
  * A numerical value that indicates the priority for the rule. The lower the number, the higher the priority. A rule with a priority of 1 is evaluated first. Priorities must be unique for each rule. A good practice is to give rules priority numbers that allow later insertion (such as 100, 200, 300).
* `--direction`:
  * The direction of traffic, either ingress or egress.
* `--action`:
  * The action on match, either allow or deny. Must be used with the `--rules` flag.
* Specify a target in one of three ways:
  * Omit `--target-tags` and `--target-service-accounts` if the rule should apply to all targets in the network.
  * `--target-tags` Use this flag to define targets by network tags.
  * `--target-service-accounts` Use this flag to define targets by associated service accounts.
* For an ingress rule, specify a source:
  * Omit `--source-ranges`, `source-tags`, and `--source-service-accounts` if the ingress source should be everywhere, 0.0.0.0/0.
  * `--source-ranges` Use this flag to specify ranges of source IP addresses in CIDR format.
  * `--source-tags` Use this flag to specify source instances by network tags. Filtering by source tag is only available if the target is not specified by service account. For more information, see filtering by service account vs. network tag.
  * `--source-ranges` and `--source-tags` can be used together. If both are specified, the effective source set is the union of the source range IP addresses and the instances identified by network tags, even if the tagged instances do not have IPs in the source ranges.
  * `--source-service-accounts` Use this flag to specify instances by the service accounts they use. Filtering by source service account is only available if the target is not specified by network tag. For more information, see [filtering by service account vs. network tag](https://cloud.google.com/vpc/docs/firewalls#service-accounts-vs-tags). `--source-ranges` and `--source-service-accounts` can be used together. If both are specified, the effective source set is the union of the source range IP addresses and the instances identified by source service accounts, even if the instances identified by source service accounts do not have IPs in the source ranges.
* For an egress rule, specify a destination:
  * Omit `--destination-ranges` if the egress destination should be anywhere, 0.0.0.0/0.
  * `--destination-ranges` Use this flag to specify ranges of destination IP addresses in CIDR format.
* `--rules`:
  *  A list of protocols and ports to which the rule will apply. Use all to make the rule applicable to all protocols and all ports. Requires the `--action` flag.
* By default, firewall rules are created and enforced automatically; however, you can change this behavior.
  * If both `--disabled` and `--no-disabled` are omitted, the firewall rule is created and enforced.
  * `--disabled` :
    * Add this flag to create the firewall rule but not enforce it. The firewall rule will remain in a disabled state until you update the firewall rule to enable it.
  * `--no-disabled`:
    * Add this flag to ensure the firewall rule is enforced.
* `--enable-logging` | `--no-enable-logging` :
  * You can enable Firewall Rules Logging for a rule when you create or update it. Firewall Rules Logging allows you audit, verify, and analyze the effects of your firewall rules. See [Firewall Rules Logging](https://cloud.google.com/vpc/docs/firewall-rules-logging) for details.
* `--logging-metadata`:
  * If you enable logging, by default, Firewall Rules Logging includes base and metadata fields. You can omit metadata fields to save storage costs.

To update a firewall rule use the following command:
```
gcloud compute firewall-rules update NAME \
    [--priority=PRIORITY] \
    [--description=DESCRIPTION] \
    [--target-tags=TAG,...] \
    [--target-service-accounts=IAM Service Account, ...] \
    [ --source-ranges=CIDR-RANGE, ...] \
    [--source-tags=TAG,...] \
    [--source-service-accounts=IAM Service Account, ...] \
    [--destination-ranges=CIDR-RANGE, ...] \
    [--rules=[PROTOCOL[:PORT[-PORT]],…]] \
    [--disabled | --no-disabled] \
    [--enable-logging | --no-enable-logging]
```

#### GCP Allows Blocked Traffic
The following is network traffic that is allways blocked by GCP and firewall rules **CANNOT** unblock them:

|Blocked Traffic| Applies To|
|---------------|-----------|
| GRE Traffic| all sources, all destinations, including among instances using internal ip addresses, unless explicitly allowed through protocol forwarding|
| Protocols othan than TCP, UDP, ICMP, and IPIP| Traffic between: * instances and the internet * instances if they are addressed with external IPs * instances if a load balancer with an external IP is involved |
| Egress Traffic on TCP port 25 (SMTP) | Traffic From: * instances to the internet * instances to other instances addressed by external IPs |
| Egress Traffic on TCP port 465 or 587 (SMTP over SSL/TLS) | Traffic from: * instances to the internet, except for traffic destined for known Google SMTP Servers * instances to other instances addressed by external IPs |


**Filter by service account vs network tag**
If you need strict control over how firewall rules are applied to VMs, use target service accounts and source service accounts instead of target tags and source tags:
  * A network tag is an arbitrary attribute. One or more network tags can be associated with an instance by any Identity and Access Management (IAM) member who has permission to edit it. IAM members with the Compute Engine Instance Admin role to a project have this permission. IAM members who can edit an instance can change its network tags, which could change the set of applicable firewall rules for that instance.
  * A service account represents an identity associated with an instance. Only one service account can be associated with an instance. You control access to the service account by controlling the grant of the Service Account User role for other IAM members. For an IAM member to start an instance by using a service account, that member must have the Service Account User role to at least that service account and appropriate permissions to create instances (for example, having the Compute Engine Instance Admin role to the project).

You cannot mix filtering by service account or network tags.

[Configure VPC Firewall Lab](https://docs.google.com/document/d/1RWck9O9sJt6SqI1l8o9-aih2UPwLmp6dQQFbvoWmNwM/edit#heading=h.2g4x5d8r4egh)

### VPC Peering
Google Cloud VPC Network Peering allows internal IP address connectivity across two Virtual Private Cloud (VPC) networks regardless of whether they belong to the same project or the same organization.

VPC Network Peering enables you to connect VPC networks so that workloads in different VPC networks can communicate internally. Traffic stays within Google's network and doesn't traverse the public internet.

VPC Network Peering is useful for:

* SaaS (Software-as-a-Service) ecosystems in Google Cloud. You can make services available privately across different VPC networks within and across organizations.
* Organizations with several network administrative domains can peer with each other.

VPC Network Peering gives you several advantages over using external IP addresses or VPNs to connect networks, including:

* Network Latency:
  * Connectivity that uses only internal addresses provides lower latency than connectivity that uses external addresses.
* Network Security:
  * Service owners do not need to have their services exposed to the public Internet and deal with its associated risks.
* Network Cost:
  * Google Cloud charges egress bandwidth pricing for networks using external IPs to communicate even if the traffic is within the same zone. If however, the networks are peered they can use internal IPs to communicate and save on those egress costs. Regular network pricing still applies to all traffic.

**Peering is NOT supported for legacy networks.**

Only directly peered networks can communicate. Transitive peering is not supported. In other words, if VPC network N1 is peered with N2 and N3, but N2 and N3 are not directly connected, VPC network N2 cannot communicate with VPC network N3 over VPC Network Peering.

At the time of peering, Google Cloud checks to see if there are any subnets with overlapping IP ranges between the two VPC networks or any of their peered networks. If there is an overlap, peering is not established. Since a full mesh connectivity is created between VM instances, subnets in the peered VPC networks can't have overlapping IP ranges as this would cause routing issues.

![network peering](images/network-peering-11.svg)

### VPC Flow Logs
VPC Flow Logs records network flows sent from or received by VM instances.
VPC flow logs will only include traffic seen by a VM (e.g., if traffic was blocked by an egress rule, it will be seen but traffic blocked by an ingress rule, not reaching a VM, will not be seen.
The traffic will include:
* Network flows between VMs in the same VPC
* Network flows between VMs in a VPC network and hosts in your on-premises network that are connected via VPN or Cloud Interconnect
* Network flows between VMs and end locations on the Internet
* Network flows between VMs and Google services in production Protocols: you can monitor network flows for TCP and UDP.

These logs can be used to monitor network traffic to and from your VMs, forensics, real-time security analysis, and expense optimization.

You can view flow logs in Stackdriver Logging, and you can export logs to any destination that Stackdriver Logging export supports (Cloud Pub/Sub, BigQuery, etc.).

Flow logs are aggregated by connection, at a 5 second interval, from Compute engine instances and exported in real time. By subcribing to Cloud Pub/Sub, you can analyze flow logs using real-time streaming.

VPC Flow Logs service is disabled by default on all VPC subnets. When enabled it applies to all VM instances in the subnet.

Enabling VPC Flow Logs can be done via Console (at any time) using the CLI.

```
gcloud compute networks subnets create|update subnet-name \
    --enable-flow-logs \
    [--logging-aggregation-interval=aggregation-interval \
    [--logging-flow-sampling=0.0...1.0] \
    [--logging-filter-expr=expression] \
    [--logging-metadata=(include-all | exclude-all | custom)] \
    [--logging-metadata-fields=fields] \
    [other flags as needed]
```

[VPC Flow Log Lab](https://www.qwiklabs.com/focuses/1236?parent=catalog)

For more details on VPC Peering go [here](https://cloud.google.com/vpc/docs/vpc-peering)

[VPC Peering Lab](https://www.qwiklabs.com/focuses/964?parent=catalog)

### Shared VPC
Shared VPC allows an organization to connect resources from multiple projects to a common Virtual Private Cloud (VPC) network, so that they can communicate with each other securely and efficiently using internal IPs from that network. When you use Shared VPC, you designate a project as a host project and attach one or more other service projects to it. The VPC networks in the host project are called Shared VPC networks. [Eligible resources](https://cloud.google.com/vpc/docs/shared-vpc#resources_that_can_be_attached_to_shared_vpc_networks_from_a_service_project) from service projects can use subnets in the Shared VPC network.

**Shared VPC Admin**
Shared VPC Admins have the Compute Shared VPC Admin `compute.xpnAdmin` and Project IAM Admin `resourcemanager.projectIamAdmin` roles for the organization or one or more folders. They perform various tasks necessary to set up Shared VPC, such as enabling host projects, attaching service projects to host projects, and delegating access to some or all of the subnets in Shared VPC networks to Service Project Admins. A Shared VPC Admin for a given host project is typically its project owner as well.
A user assigned the Compute Shared VPC Admin role for the organization has that role for all folders in the organization. A user assigned the role for a folder has that role for the given folder and any folders nested underneath it. A Shared VPC Admin can link projects in two different folders only if the admin has the role for both folders.

**Service Project Admins**
When defining each Service Project Admin, a Shared VPC Admin can grant permission to use the whole host project or just some subnets:

* Project-level permissions:
  * A Service Project Admin can be defined to have permission to use all subnets in the host project if the Shared VPC Admin grants the role of `compute.networkUser` for the whole host project to the Service Project Admin. The result is that the Service Project Admin has permission to use all subnets in all VPC networks of the host project, including subnets and VPC networks added to the host project in the future.

* Subnet-level permissions:
  * Alternatively, a Service Project Admin can be granted a more restrictive set of permissions to use only some subnets if the Shared VPC Admin grants the role of `compute.networkUser` for those selected subnets to the Service Project Admin. A Service Project Admin who only has subnet-level permissions is restricted to using only those subnets. After new Shared VPC networks or new subnets are added to the host project, a Shared VPC Admin should review the permission bindings for the compute.networkUser role to ensure that the subnet-level permissions for all Service Project Admins match the intended configuration.

**Network and Security Admins**
Shared VPC Admins have full control over the resources in the host project, including administration of the Shared VPC network. They can optionally delegate certain network administrative tasks to other IAM members:
* Network Admin:
  * Shared VPC Admin define a Network Admin by granting an IAM member the Network Admin `compute.networkAdmin` role to the host project. Network Admins have full control over all network resources except for firewall rules and SSL certificates. The network admin role does not allow a user to create, start, stop, or delete instances
* Network User (`compute.networkUser`):
  * Can use VPC networks and subnets that belong to the host project (Shared VPC). For example, a network user can create a VM instance that belongs to a host project network but they cannot delete or create new networks in the host project.
* Network Viewer (`compute.networkViewer`):
  * Read-only access to all networking resources.
* Security Admin:
  * A Shared VPC Admin can define a Security Admin by granting an IAM member the Security Admin `compute.securityAdmin` role to the host project. Security Admins manage firewall rules and SSL certificates.

**Project Structure**
Shared VPC connects projects within the same organization. Participating host and service projects cannot belong to different organizations. Linked projects can be in the same or different folders, but if they are in different folders the admin must have Shared VPC Admin rights to both folders. Refer to the Google Cloud resource hierarchy for more information about organizations, folders, and projects.

A project that participates in a Shared VPC can be either:

* a **_host_** project:
  * contains one or more Shared VPC networks. A Shared VPC Admin must first enable a project as a host project. After that, a Shared VPC Admin can attach one or more service projects to it.
* a **_service_** project:
  * is any project that has been attached to a host project by a Shared VPC Admin. This attachment allows it to participate in Shared VPC. It's a common practice to have multiple service projects operated and administered by different departments or teams in your organization.

* Project-level permissions:
  * A Service Project Admin can be defined to have permission to use all subnets in the host project if the Shared VPC Admin grants the role of `compute.networkUser` for the whole host project to the Service Project Admin. The result is that the Service Project Admin has permission to use all subnets in all VPC networks of the host project, including subnets and VPC networks added to the host project in the future.
* Subnet-level permissions:
  * Alternatively, a Service Project Admin can be granted a more restrictive set of permissions to use only some subnets if the Shared VPC Admin grants the role of `compute.networkUser` for those selected subnets to the Service Project Admin. A Service Project Admin who only has subnet-level permissions is restricted to using only those subnets. After new Shared VPC networks or new subnets are added to the host project, a Shared VPC Admin should review the permission bindings for the `compute.networkUser` role to ensure that the subnet-level permissions for all Service Project Admins match the intended configuration.

## 2.2.b - Configuring network isolation and data encapsulation for N tier application design

If you want complete isolation between various applications, customers, etc., you could create multiple networks.

![multiple vpc](images/multiple-networks-1024x646.png)

You can have up to five networks per project, including the default network. Multiple networks within a single project can provide multi-tenancy, IP overlap, or isolation within the project itself. Just another option instead of having multiple projects.

## 2.2.c - Configuring app-to-app security policy

### VPC
Leverage firewall rules using tags, or specific service accounts with fine grained access.

Allow traffic from specific networks


### App Engine Security Best Practices

Leverage Identity-Aware Proxy (IAP) for limiting access to specific users [Configure IAP For GAE](https://www.qwiklabs.com/focuses/5562?parent=catalog#).
* Use internal for G-Suite accounts
* Use External for G-Mail accounts

**HTTPS Requests**
Use HTTPS requests to access to your App Engine app securely. Depending on how your app is configured, you have the following options:
* default service:
  * https://[PROJECT_ID].[REGION_ID].r.appspot.com
* specific service
  * https://[VERSION_ID]-dot-[SERVICE_ID]-dot-[PROJECT_ID].[REGION_ID].r.appspot.com

You can force HTTPS all the time by leveraging _Handlers_ within the application YAML configuration as shown below:

```
runtime: go112 # replace with go111 for Go 1.11
service: test-app
handlers:
- url: /.*
  script: auto
  secure: always
  redirect_http_response_code: 301

```
**Access Control**
Leverage IAM Access to restrict or grant access to App Engine. Assign roles to either service accounts or users.

IAM Roles:
* App Engine Admin:
  * R/W/M all app configs and settings
  * Use cases:
    * Owner/Admin
    * On-call engineer
    * Sys Admin
* App Engine Service Admin:
  * R/O access to app configs/settings
  * Write access to service-level and version-level settings, including traffic configs.
  * Cannot deploy
  * Use Cases:
    * Release Engineer
    * DevOps
    * On-call engineer
    * Sys Admin
* App Engine Deployer
  * R/O to all app configs and settings
  * Write access only to deploy and create new versions
  * Delete Old versions that are not serving traffic
  * Cannot modify and existing version, nor change traffic configs
  * Use Cases:
    * Deployment account
    * Release engineer
* App Engine View
  * R/O to all app configs and settings
  * Use Cases:
    * Need visibility into app but no need to modify
    * Audit type of role for compliance checking
* App Engine Code Viewer
  * R/O to all app configs, settings and deployed source code
  * Use Cases:
    * Need visibility into the app and its source code but not modify it
    * DevOps User needing to diagnose prod issues.

**App Engine Firewall**
Enables you to control access to your App Engine app through a set of rules that can either allow or deny requests from the specified ranges of IP addresses.

[Controlling App Access with Firewalls](https://cloud.google.com/appengine/docs/standard/go/creating-firewalls)

**Ingress Controls**
By default, your App Engine app receives all HTTP requests that are sent to its appspot URL or to a custom domain that you have configured for your app. For apps in the flexible environment, this includes requests from the internet as well as requests from a Virtual Private Cloud network that you can create in your app's Google Cloud project. Apps in the standard environment do not receive requests from a Virtual Private Cloud (VPC).

Use App Engine firewalls see https://cloud.google.com/appengine/docs/legacy/standard/python/application-security


</details>

<details>
<summary> 2.3 Establishing private connectivity</summary>

## 2.3.a - Designing and configuring private RFC1918 connectivity between VPC networks and Google Cloud Projects (Shared VPC, VPC perring)

## 2.3.b - Designing and configuring RFC1918 connectivity between data centers and VPC network (IPSec and Cloud Interconnect)


### Cloud VPN
Cloud VPN securely connects your peer network to your Virtual Private Cloud (VPC) network through an [IPsec](https://wikipedia.org/wiki/IPsec) [VPN](https://wikipedia.org/wiki/Virtual_private_network) connection.

Traffic traveling between the two networks is encrypted by one VPN gateway, and then decrypted by the other VPN gateway. This protects your data as it travels over the internet. You can also connect two instances of Cloud VPN to each other.

![IPSec VPN](images/IPSec%20VPN.png)

Offers an SLA of 99.9% service availability. Supports Static Routes or Dynamic Routes (with the use of a Cloud Router). Suppports IKEv1 and IKEv2 (Internet Key Exchange) using a shared secret (IKE preshared key).

Cloud VPN traffic will either traverse the public internet or can use a direct peering line to Google's Network (Cloud Interconnect).

Each _Cloud VPN_ can support up to 3 Gbs when traffic is traversing a direct pering link, or 1.5 Gps when its traversing over the public internet.

**VPN with Static Routes**
![VPN Static Routes](images/VPN_Static_Routes.png)

**VPN with Dynamic Routes using Cloud Router**
![VPN Dynamic Routes](images/VPN_Dynamic_Routes.png)

New subnets in GCP or in the Peer network are discovered and shared, enabling connectivity between the two peers for both entire networks. No restart required.

Two types of configuration
* VPN Gateway:
  * A virtual VPN gateway running in Google Cloud managed by Google, using a configuration that you specify in your project, and used only by you. Each Cloud VPN gateway is a regional resource that uses one or more regional external IP addresses. A Cloud VPN gateway can connect to a peer VPN gateway.
* HA (High Availability) or Classic VPN
  * HA:
    * Supports only BGP (Border Gateway Protocol) Routing
    * External IPs created from a pool. No forwarding rules required.
    * Provides a 99.99% SLA when configured with two interfaces and two external IPs
* Classic:
  * External IPs and forwarding rules must be created
  * Static Routing (policy based, route based) or Dynamic Routing using BGP
  * Provides a 99.99% SLA

### Cloud Interconnect
Network Connectivity provides two options for extending your on-premises network to your VPC networks in Google Cloud. You can create a dedicated connection (Dedicated Interconnect) or use a service provider (Partner Interconnect) to connect to VPC networks. When choosing an interconnect type, consider your connection requirements, such as the connection location and capacity.

Cloud Interconnect provides low latency, high availability connections that enable you to reliably transfer data between you on-premise network and GCP VPC Networks.

Types:
* Dedicated Interconnect:
  * A direct connection to Google.
  * Traffic flows directly between networks, not through the public internet.
  * Availble in 10 Gbps or 100 Gbps circuits with flexible interconnect attachment (VLAN) capacities from 50 Mbps to 50 Gbps.
  * BGP must be configured on your on-prem routers as well as Cloud Routers
  * Google Provides end-to-end SLA
* Partner Interconnect:
  * More points of connectivity through one of our supported service providers.
  * Traffic flows between networks through a service provider, not through the public internet.
  * Flexible capacities from 50 Mbps to 50 Gbps.
  * For layer 2 connections, BGP must be configured on your on-prem routers and Cloud Routers
  * For Layer 3 connections, the configurations of Cloud Routers and their peers is fully automated.
  * Google provides and SLA between Google Network and the Service Provider. E2E SLA depends on the Service Provider.

Choosing the right connection:
![Choose Cloud Interconnect](images/choose-interconnect.png)

## 2.3.c - Establishing priving connectivity between VPC and Google APIs (Private Google Access, Private Google Access for on-premises hosts, Private Service Connect)

### Private Google API Access
Private Google API Access enables Compute Engine instances on a VPC subnet to reach
Google APIs and services using an internal IP address rather than an external IP address. Previously, you had to provide a public path for your internal Compute Engine instances (for example, an external IP address or a NAT gateway) to allow the instances to access Google APIs.

With Private Google Access, an API call is resolved to a public IP address, but the traffic is all internal and private. Network address translation is in Google's infrastructure and is transparent
to the user.

If Private Google Access is not enabled, an organization requires an external IP address to communicate with Google APIs. Although the communication is encrypted, this IP address can increase an organization’s risk by unnecessarily exposing its network to the internet.

When Private Google Access is enabled, VM instances in a subnet can reach the above APIs and services without needing an external IP address. Instead, VMs can use their internal IP addresses to access Google managed services.

Instances with external IP addresses are not affected when you enable the ability to access Google services from internal IP addresses. These instances can still connect to Google APIs and managed services.

The Cloud and Developer APIs and services that can be reached include, but are not limited to, the following:
* BigQuery
* Cloud Bigtable
* Container Registry
* Cloud Dataproc
* Cloud Datastore
* Cloud Pub/Sub
* Cloud Spanner
* Cloud Storage

Private Google Access does not apply to Cloud SQL. You do not get private connectivity to Cloud SQL when you use Private Google Access.

Private Goolge API access is enabled on VPC subnets but is disabled by default. You add this feature to your projects when you create a subnetwork or by modifying an existing subnetwork.

You must also ensure that any Compute Engine instance that accesses a Google API has a matching `default-internet-gateway` route set in its GCP-based network. All GCP networks have a `default-internet-gateway` route, unless the route has been manually deleted.

[Private Google Access and Cloud NAT Lab](https://www.qwiklabs.com/focuses/4362?parent=catalog)

* private.googleapis.com (199.36.153.8/3)
  * Enables API access to most Google APIs and services regardless of whether they are supported by VPC Service Controls. Includes API access to Maps, Google Ads, Google Cloud platform, and most other Google APIs, including the lists below. Does not support G Suite web applications.
  * Use private.googleapis.com to access Google APIs and services using a set of IP addresses only routable from within Google Cloud. Choose private.googleapis.com under these circumstances:
  * You don't use VPC Service Controls.
  * You do use VPC Service Controls, but you also need to access Google APIs and services that are not supported by VPC Service Controls.
* restricted.googleapis.com (199.36.153.4/30)
  * Use restricted.googleapis.com to access Google APIs and services using a set of IP addresses only routable from within Google Cloud. Choose restricted.googleapis.com when you only need access to Google APIs and services that are supported by VPC Service Controls — restricted.googleapis.com does not permit access to Google APIs and services that do not support VPC Service Controls.

#### Private Google Access to on-premises hosts

Private Google Access for on-premises hosts provides a way for on-premises systems to connect to Google APIs and services by routing traffic through a Cloud VPN tunnel or a VLAN attachment for Cloud Interconnect. Private Google Access for on-premises hosts is an alternative to connecting to Google APIs and services over the internet.

[Configuring Private Google Access for on-prem hosts](https://cloud.google.com/vpc/docs/configure-private-google-access-hybrid)

### Serverless VPC Access

Serverless VPC Access enables you to connect from a serverless environment on Google Cloud (Cloud Run (fully managed), Cloud Functions, or the App Engine standard environment) directly to your VPC network. This connection makes it possible for your serverless environment to access Compute Engine VM instances, Memorystore instances, and any other resources with an internal IP address. For example, this can be helpful in the following cases:

* You use Memorystore to store data for a serverless service.
* Your serverless workloads use third-party software that you run on a Compute Engine VM.
* You run a backend service on a Managed Instance Group in Compute Engine and need your serverless environment to communicate with this backend without exposure to the public internet.
* Your serverless environment needs to access data from your on-premises database through Cloud VPN.

Connection to a VPC network enables your serverless environment to send requests to internal DNS names and internal IP addresses as defined by RFC 1918 and RFC 6598. These internal addresses are only accessible from Google Cloud services. Using internal addresses avoids exposing resources to the public internet and improves the latency of communication between your services.

#### Serverless VPC Access Connector
Serverless VPC Access is based on a resource called a connector. A connector handles traffic between your serverless environment and your VPC network. When you create a connector in your Google Cloud project, you attach it to a specific VPC network and region. You can then configure your serverless services to use the connector for outbound network traffic.

When you create a connector, you also assign it an IP range. Traffic sent through the connector into your VPC network will originate from an address in this range. The IP range must be a CIDR /28 range that is not already reserved in your VPC network. An implicit firewall rule with priority 1000 is created on your VPC network to allow ingress from the connector's IP range to all destinations in the network.

Serverless VPC Access automatically provisions throughput for a connector in 100 Mbps increments depending on the amount of traffic sent through the connector. Automatically provisioned throughput can only scale up and does not scale down. A connector always has at least 200 Mbps provisioned and can scale up to 1000 Mbps. You can configure throughput scaling limits when you create a connector; note that actual throughput through a connector may exceed the provisioned throughput, especially for short traffic bursts.

IAM Roles:

* Serverless VPC Access Admin
  * `roles/vpcaccess.admin`
  * Full access to all Serverless VPC Access resources
* Serverless VPC Access User
  * `roles/vpcaccess.user`
  * User of Serverless VPC Access connectors
* Serverless VPC Access Viewer
  * `roles/vpcaccess.viewer`
  * Viewer of all Serverless VPC Access resources

![Serverless VPC Access](images/serverless-vpc-access.svg)

To enable this feature you need to enable the API:

```
gcloud services enable vpcaccess.googleapis.com
```

To create the connector:

```
gcloud compute networks vpc-access connectors create [CONNECTOR_NAME] \
--network [VPC_NETWORK] \
--region [REGION] \
--range [IP_RANGE]
```

[Configure VPC Access for CLoud Run](https://cloud.google.com/run/docs/configuring/connecting-vpc)
[Configure VPC Access for Cloud Functions](https://cloud.google.com/functions/docs/connecting-vpc)
Configure VPC Access for App Engine Standard](https://cloud.google.com/appengine/docs/standard/python/connecting-vpc)

## 2.3.d - Configuring Cloud NAT

Cloud NAT (network address translation) allows Google Cloud virtual machine (VM) instances without external IP addresses and private Google Kubernetes Engine (GKE) clusters to send outbound packets to the internet and receive any corresponding established inbound response packets.

Cloud NAT is a distributed, software-defined managed service. It's not based on proxy VMs or appliances. Cloud NAT configures the [Andromeda software](https://cloudplatform.googleblog.com/2014/04/enter-andromeda-zone-google-cloud-platforms-latest-networking-stack.html) that powers your VPC network so that it also provides source network address translation (SNAT) for VMs without external IP addresses. Cloud NAT also provides destination network address translation (DNAT) for established inbound response packets.

![traditional nat vs cloud nat](images/trad-nat-v-cloud-nat.svg)

Cloud NAT implements outbound NAT in conjunction with static routes in your VPC whose next hops are default internet gateway. In a basic configuration, a default route in your VPC network meets this requirement.

Cloud NAT does not implement unsolicited inbound connections from the internet. DNAT is only performed for packets that arrive as responses to outbound packets.

**Creating a Cloud Router**

NOTE the Cloud Router MUST be in the same region as the instance using  Cloud NAT


```
gcloud compute routers create nat-router \
    --network custom-network1 \
    --region us-east4
```

Add a NATS configuration to the router:

```
gcloud compute routers nats create nat-config \
    --router-region us-east4 \
    --router nat-router \
    --nat-all-subnet-ip-ranges \
    --auto-allocate-nat-external-ips \
    --enable-logging
```


</details>






[^1]: Taken from the GCP Security Engineer Documentation (https://cloud.google.com/certification/cloud-security-engineer)

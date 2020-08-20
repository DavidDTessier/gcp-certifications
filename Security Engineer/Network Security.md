# Network Security
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

### Firewalls
Virtual Private Cloud (VPC) firewall rules apply to a given project and network. You can create a [Firewall Policy](https://cloud.google.com/vpc/docs/firewall-policies) that can be applied accross the orgranization.

VPC firewall rules let you allow or deny connections to or from your virtual machine (VM) instances based on a configuration that you specify. Enabled VPC firewall rules are always enforced, protecting your instances regardless of their configuration and operating system, even if they have not started up.

Every VPC network functions as a distributed firewall. While firewall rules are defined at the network level, connections are allowed or denied on a per-instance basis. You can think of the VPC firewall rules as existing not only between your instances and other networks, but also between individual instances within the same network.

In addition to firewall rules that you create, Google Cloud has other rules that can affect incoming (ingress) or outgoing (egress) connections:

* Google Cloud doesn't allow certain IP protocols, such as egress traffic on TCP port 25 within a VPC network.
* Google Cloud always allows communication between a VM instance and its corresponding metadata server at 169.254.169.254. 
* Every network has two implied firewall rules that permit outgoing connections and block incoming connections. Firewall rules that you create can override these implied rules.
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
  * A numerical value that indicates the priority for the rule. The lower the number, the higher the priority.
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

**Filter by service account vs network tag**
If you need strict control over how firewall rules are applied to VMs, use target service accounts and source service accounts instead of target tags and source tags:

A network tag is an arbitrary attribute. One or more network tags can be associated with an instance by any Identity and Access Management (IAM) member who has permission to edit it. IAM members with the Compute Engine Instance Admin role to a project have this permission. IAM members who can edit an instance can change its network tags, which could change the set of applicable firewall rules for that instance.

A service account represents an identity associated with an instance. Only one service account can be associated with an instance. You control access to the service account by controlling the grant of the Service Account User role for other IAM members. For an IAM member to start an instance by using a service account, that member must have the Service Account User role to at least that service account and appropriate permissions to create instances (for example, having the Compute Engine Instance Admin role to the project).

You cannot mix filtering by service account or network tags.

## VPC Peering
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

For more details on VPC Peering go [here](https://cloud.google.com/vpc/docs/vpc-peering)

## Shared VPC
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
  * Shared VPC Admin define a Network Admin by granting an IAM member the Network Admin `compute.networkAdmin` role to the host project. Network Admins have full control over all network resources except for firewall rules and SSL certificates.
* Security Admin:
  * A Shared VPC Admin can define a Security Admin by granting an IAM member the Security Admin `compute.securityAdmin` role to the host project. Security Admins manage firewall rules and SSL certificates.

**Project Strucuture**
Shared VPC connects projects within the same organization. Participating host and service projects cannot belong to different organizations. Linked projects can be in the same or different folders, but if they are in different folders the admin must have Shared VPC Admin rights to both folders. Refer to the Google Cloud resource hierarchy for more information about organizations, folders, and projects.

A project that participates in a Shared VPC can be either:

* a **_host_** project:
  * contains one or more Shared VPC networks. A Shared VPC Admin must first enable a project as a host project. After that, a Shared VPC Admin can attach one or more service projects to it.
* a **_service_** project:
  * is any project that has been attached to a host project by a Shared VPC Admin. This attachment allows it to participate in Shared VPC. It's a common practice to have multiple service projects operated and administered by different departments or teams in your organization.

## Cloud NAT
Cloud NAT (network address translation) allows Google Cloud virtual machine (VM) instances without external IP addresses and private Google Kubernetes Engine (GKE) clusters to send outbound packets to the internet and receive any corresponding established inbound response packets.

Cloud NAT is a distributed, software-defined managed service. It's not based on proxy VMs or appliances. Cloud NAT configures the [Andromeda software](https://cloudplatform.googleblog.com/2014/04/enter-andromeda-zone-google-cloud-platforms-latest-networking-stack.html) that powers your VPC network so that it also provides source network address translation (SNAT) for VMs without external IP addresses. Cloud NAT also provides destination network address translation (DNAT) for established inbound response packets.

![traditional nat vs cloud nat](images/trad-nat-v-cloud-nat.svg)

Cloud NAT implements outbound NAT in conjunction with static routes in your VPC whose next hops are default internet gateway. In a basic configuration, a default route in your VPC network meets this requirement.

Cloud NAT does not implement unsolicited inbound connections from the internet. DNAT is only performed for packets that arrive as responses to outbound packets.

## Cloud DNS & DNS Security
Cloud DNS
DNSSEC is a feature of the Domain Name System that authenticates responses to domain name lookups. It does not provide privacy protections for those lookups, but prevents attackers from manipulating or poisoning the responses to DNS requests.

DNSSec needs to be enabled in the following three places:

1. The DNS zone for your domain must serve special DNSSEC records for public keys (DNSKEY), signatures (RRSIG), and non-existence (NSEC, or NSEC3 and NSEC3PARAM) to authenticate your zone's contents. Cloud DNS manages this automatically if you [enable DNSSEC for a zone](https://cloud.google.com/dns/docs/dnssec-config#enabling).
2. The top-level domain registry (for example.com, this would be .COM) must have a DS record that authenticates a DNSKEY record in your zone. Do this by activating DNSSEC at your domain registrar.
3. For full DNSSEC protection, clients must use a DNS resolver that validates signatures for DNSSEC-signed domains. You can enable validation for individual systems or the local DNS resolvers (Refer to the appendices in this PDF guide). You can also configure systems to use public resolvers that validate DNSSEC, notably Google Public DNS and Verisign Public DNS.
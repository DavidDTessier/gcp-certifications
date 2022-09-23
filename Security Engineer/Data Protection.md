
# Application Security Vulnerabilities
Application security is often undefined and untested.

Attackers will attack applications more often than any other target - more often than networks and infrastructure.

The most common app vulnerabilities are:
* Injection:
  * SQL Injection
  * LDAP injection
  * HTML Injection
* Cross-site scripting (XSS)
* Weak authentication and access control
* Secure data exposure
* Security misconfiguration
* Using components with known vulnerabilities


# Identity-Aware Proxy (IAP)
Is a central authentication and authorization layer that can be used for applications over HTTPS. It also replaces end-user VPN tunnels or the need to apply an authentication and authorization layer in from of web apps hosted in GCP.

![Cloud IAP](images/Cloud%20IAP.png)

When a user logs into the application the request is forwarded to the Cloud IAP proxy which requires the user to log in. The proxy will determine if the user is allowed to access the application, if they are, they are then forwarded to the requested app page.

Labs:

[User Authentication with Identity Aware Proxy](https://www.qwiklabs.com/focuses/5562?parent=catalog)







# Security Partner Products
GCP offers best in class platfrom security they also have a fast partner ecosystem that provide security-centric products.

See the list [here](https://cloud.google.com/security/partners#tab1) for more details.

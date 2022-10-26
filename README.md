# Okta Test

## SPA setup

https://developer.okta.com/docs/guides/embedded-siw/main/

* Security > API
  * Trusted Origins: OktaTest, https://localhost:5001, CORS and redirect
* App > App
  * new integration: OIDC, SPA, check auth code, interaction code and refresh token, set redirects to https://localhost:5001/authtest, allow everyone
  * Edit app general, email verification https://localhost:5001/authtest
  * Edit app sign on, user authentication policy to password only
* Security > Authenticators
  * Edit password authenticator, edit default rule additional verification not required.
* Security > API
  * Add custom auth server: OktaTest, aud https://localhost:5001
  * Edit Policies, add default policy, check client credentials, auth code, interation code, implicit, resource owner password, the rest should match the default custom server.
* Make a test user and group

## Server setup

https://developer.okta.com/docs/guides/protect-your-api/main/


## TODO

* Fix logout not working
* Get groups on server Identity
* get user name on server identity
* make policy for specific group

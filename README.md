# Okta Test

## SPA setup

<https://developer.okta.com/docs/guides/embedded-siw/main/>

* Security > API
  * Trusted Origins: OktaTest, <https://localhost:5001>, CORS and redirect
* App > App
  * new integration: OIDC, SPA, check auth code, interaction code and refresh token, set redirects to <https://localhost:5001/authtest>, allow everyone
  * Edit app general, email verification <https://localhost:5001/authtest>
  * Edit app sign on, user authentication policy to password only
* Security > Authenticators
  * Edit password authenticator, edit default rule additional verification not required.
* Security > API
  * Add custom auth server: OktaTest, aud <https://localhost:5001>
  * Edit Access Policies, add default policy all clients
  * Add default policy rule, check client credentials, auth code, interaction code, implicit, resource owner password, Refresh token lifetime of 90 days, the rest should match the default custom server.
  * Add claim "groups", access token, value: Groups, Matches Regex .*, Any scope
* Make a test user and group

## Server setup

<https://developer.okta.com/docs/guides/protect-your-api/main/>

## TODO

* set cookie so MVC works <https://developer.okta.com/docs/guides/sign-into-web-app-redirect/asp-net-core-3/main/#get-info-about-the-user>
* need to setup token refreshes, am signed out after an hour

## Observations

Claims are not refreshed from Okta upon every request. This means if a user is added or removed from a group, they will have to sign out and back in to get their new claims or wait until the access token expires (see Authorization Server Access Policies for how long these last, default is 60 minutes).
The refresh token is what's used to refresh the other tokens after the hour and it has a much longer lifetime. For native apps, this is usually persistent/unlimited. For web apps, you might want them to re-sign in every 3-6 months.

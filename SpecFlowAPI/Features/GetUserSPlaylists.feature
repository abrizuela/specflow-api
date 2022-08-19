@get
Feature: Get User's Playlists
	Get a list of the playlists owned or followed by a Spotify user.
	GET /users/{user_id}/playlists

Scenario: Get User's Playlists should succeed
	When Get User's Playlists operation is executed
	Then the status code should be 'OK'

Scenario: Get User's Playlists with a nonexistent User Id should fail
	Given a nonexistent User Id is populated
	When Get User's Playlists operation is executed
	Then the status code should be 'NotFound'

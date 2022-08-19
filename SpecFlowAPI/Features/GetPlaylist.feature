@get
Feature: Get Playlist
	Get a playlist owned by a Spotify user.
	POST /users/{user_id}/playlists

Scenario: Get Playlist should succeed
	Given Random Playlist Id is populated
	When Get Playlist operation is executed
	Then the status code should be 'OK'

Scenario: Get a Playlist with a nonexistent Playlist Id should fail
	Given a nonexistent playlist Id is populated
	When Get Playlist operation is executed
	Then the status code should be 'NotFound'

[![Build Status](https://travis-ci.org/NeKzor/SteamCommunity.Net.svg?branch=master)](https://travis-ci.org/NeKzor/SteamCommunity.Net)
[![Build Version](https://img.shields.io/badge/version-v1.0-yellow.svg)](https://github.com/NeKzor/SteamCommunity.Net/projects/1)

## Usage
```cs
var client = new SteamCommunityClient(autoCache: false);

var friends = await client.GetFriendsAsync(76561198049848090u);
var list = await client.GetGamesAsync(76561198049848090u);
var global = await client.GetLeaderboardAsync("Portal 2", 49347);
var lb = await client.GetLeaderboardsAsync("Portal 2");
var group = await client.GetMemberListAsync("sourceruns");
var profile = await client.GetProfileAsync(76561198049848090u);
var stats = await client.GetStatsAsync(76561198049848090u, 620);
var feed = await client.GetStatsFeedAsync(76561198049848090u, 620);
```

## Examples
- [LeastPortals](https://nekzor.github.io/SteamCommunity.Net/lp)
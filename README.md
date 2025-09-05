# .NET Library for OpenAI SIP End Point

This repository contains a .NET library for interacting with [OpenAI's real-time SIP API](https://platform.openai.com/docs/guides/realtime-sip). It provides helper classes to establish SIP calls with the endpoint.

This library is currently a work in progress and there is no official release yet.

## Status (05 Sep 2025)

The example in `examples/GetStarted` is now working end-to-end:
- Places a SIP TLS call to `sip.api.openai.com` using your OpenAI Project ID.
- Receives the webhook (after you expose `/webhook` publicly, e.g. via ngrok).
- Accepts the call and upgrades to a realtime WebSocket session.
- Sends an initial instruction and logs incoming realtime events.

See the README in `examples/GetStarted` for setup (environment variables, ngrok, run instructions).

> Note: Audio currently negotiates PCM; Opus not yet established in testing. No echo cancellation—use a headset.

## License

Distributed under the BSD 3‑Clause license with an additional BDS BY‑NC‑SA restriction. See [LICENSE.md](https://github.com/sipsorcery-org/SIPSorcery.OpenAI.SIP/tree/main/LICENSE.md) for details.

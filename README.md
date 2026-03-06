# Wolverine — Mediator & Azure Service Bus Sample

> A hands-on reference implementation using [Wolverine](https://wolverinefx.io) as a single, unified messaging framework — replacing both MediatR (in-process) and MassTransit (distributed) — with full Azure Service Bus integration via queues, topics, and subscriptions.

---

## Overview

This repository demonstrates how **Wolverine** can act as:

- An **in-process mediator** for CQRS commands and queries — as a drop-in replacement for **MediatR**
- A **distributed message broker** over **Azure Service Bus** queues and topics — as a drop-in replacement for **MassTransit**

All with a single framework, a single programming model, and zero infrastructure duplication.

---

## Features

- ✅ In-process `IMessageBus` as a mediator (Commands, Queries, Notifications)
- ✅ Azure Service Bus **queue** messaging (point-to-point)
- ✅ Azure Service Bus **topic/subscription** messaging (publish/subscribe)
- ✅ Convention-based message routing (no manual wiring per message)
- ✅ Handler autodiscovery (and how to disable it for explicit control)
- ✅ Clean separation between local and remote message contracts

---
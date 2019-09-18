﻿// <copyright file="DefaultTracing.cs" company="OpenTelemetry Authors">
// Copyright 2018, OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenTelemetry.Trace
{
    using OpenTelemetry.Internal;
    using OpenTelemetry.Trace.Config;
    using OpenTelemetry.Trace.Export;
    using OpenTelemetry.Trace.Internal;

    /// <summary>
    /// Class that defines the default global instances of the <see cref="Tracing"/> APIs.
    /// </summary>
    public static class DefaultTracing
    {
        /// <summary>   
        /// Gets the tracer to record spans.
        /// </summary>
        public static ITracer Tracer { get; private set; }

        /// <summary>
        /// Gets the exporter to use to upload spans.
        /// </summary>
        public static ISpanExporter SpanExporter { get; private set; }

        /// <summary>
        /// Initializes the default tracing implementation.
        /// </summary>
        public static void Init()
        {
            IEventQueue eventQueue = new SimpleEventQueue();

            Tracing.SpanExporter = Trace.Export.SpanExporter.Create();

            IStartEndHandler startEndHandler =
                new StartEndHandler(
                    Trace.Export.SpanExporter.Create(),
                    eventQueue);

            Tracing.Tracer = new Tracer(startEndHandler, TraceConfig.Default);
        }
    }
}
# Grpc-Demo
Tests Grpc protocol in .net core app

The purpose of this test is to:
1. demonstrate how a WPF app can interact with various service types
  1.1. Grpc with using the entities directly
  1.2. Grpc with mapping entities to dtos
  1.3. Json service
  1.4. Direct (local) access to core
2. Compare the above protocols to compare their performance
3. The app must be service agnostic and should not require rebuilding to change the protocol

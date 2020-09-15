// swift-tools-version:5.3
// The swift-tools-version declares the minimum version of Swift required to build this package.

import PackageDescription

let package = Package(
    name: "AMP",
    defaultLocalization: "en",
    platforms: [
        .iOS(.v11)
    ],
    products: [
        .library(name: "AMP", targets: ["AMP"])
    ],
    dependencies: [],
    targets: [
        .binaryTarget(
            name: "AMP",
            path: "AMP.xcframework"
        )
    ],
    swiftLanguageVersions: [.v5]
)

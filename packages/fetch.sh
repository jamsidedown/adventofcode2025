#!/bin/sh

curl -L 'https://github.com/Z3Prover/z3/releases/download/z3-4.15.4/Microsoft.Z3.4.15.4.nupkg' > 'Microsoft.Z3.4.15.1.nupkg'

curl -L 'https://github.com/Z3Prover/z3/releases/download/z3-4.15.4/z3-4.15.4-arm64-osx-13.7.6.zip' > 'z3-4.15.4-arm64-osx-13.7.6.zip'

unzip 'z3-4.15.4-arm64-osx-13.7.6.zip'

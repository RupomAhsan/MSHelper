#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering Nuget package build

cd src/MSHelper.LoadBalancing.Fabio/src/MSHelper.LoadBalancing.Fabio
dotnet pack -c release /p:PackageVersion=1.0.$TRAVIS_BUILD_NUMBER --no-restore -o .

echo Uploading MSHelper.LoadBalancing.Fabio package to Nuget using branch $TRAVIS_BRANCH

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
    ;;
esac
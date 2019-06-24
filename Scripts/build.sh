#! /bin/sh

project="CanYou"

echo "listing files in $(pwd)"
ls $(pwd)

echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -noUpm \
  -silent-crashes \
  -logFile \
  -projectPath "$(pwd)/" \
  -executeMethod BuildScript.Linux \
  -quit

echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -noUpm \
  -silent-crashes \
  -logFile \
  -projectPath "$(pwd)/" \
  -executeMethod BuildScript.Windows \
  -quit

echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -noUpm \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$(pwd)/" \
  -executeMethod BuildScript.OSX \
  -quit

echo "Attempting to build $project for WebGL"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -noUpm \
  -nographics \
  -silent-crashes \
  -logFile \
  -projectPath "$(pwd)/" \
  -executeMethod BuildScript.WebGL \
  -quit

echo "Listing files in $(pwd)"
ls $(pwd)
echo "Listing files in $(pwd)/Build/linux"
ls $(pwd)/Build/linux
echo "Listing files in $(pwd)/Build/osx"
ls $(pwd)/Build/osx
echo "Listing files in $(pwd)/Build/windows"
ls $(pwd)/Build/windows
echo "Listing files in $(pwd)/Build/webgl"
ls $(pwd)/Build/webgl
mv $(pwd)/Scripts/templates/index.html $(pwd)/Build/webgl/
mv $(pwd)/Scripts/templates/canyou.json $(pwd)/Build/webgl/Build/
mv $(pwd)/Build/webgl/Build/.data.unityweb $(pwd)/Build/webgl/Build/canyou.data.unityweb
mv $(pwd)/Build/webgl/Build/.wasm.code.unityweb $(pwd)/Build/webgl/Build/canyou.wasm.code.unityweb
mv $(pwd)/Build/webgl/Build/.wasm.framework.unityweb $(pwd)/Build/webgl/Build/canyou.wasm.framework.unityweb

echo 'Attempting to zip builds'
cd $(pwd)/Build
zip -r linux.zip linux/
zip -r mac.zip osx/
zip -r windows.zip windows/

#! /bin/sh

BASE_URL=https://download.unity3d.com/download_unity
HASH=38bd7dec5000
VERSION=2018.2.11f1

download() {
  file=$1
  url="$BASE_URL/$HASH/$file"
  location="$HOME/Downloads/`basename $file`"

  if [ -f $location ]; then
      echo "Skipping download, $location exists."
  else
      echo "Downloading from $url: "
      curl -o $location "$url"
  fi
}

install() {
  package=$1
  download "$package"

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package $HOME/Downloads/`basename "$package"` -target /
}

# See $BASE_URL/$HASH/unity-$VERSION-$PLATFORM.ini for complete list
# of available packages, where PLATFORM is `osx` or `win`

install "MacEditorInstaller/Unity-$VERSION.pkg"
install "MacEditorTargetInstaller/UnitySetup-Windows-Mono-Support-for-Editor-$VERSION.pkg"
install "MacEditorTargetInstaller/UnitySetup-Mac-IL2CPP-Support-for-Editor-$VERSION.pkg"
install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-$VERSION.pkg"
install "MacEditorTargetInstaller/UnitySetup-WebGL-Support-for-Editor-$VERSION.pkg"

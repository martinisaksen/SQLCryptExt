$Assembly = ".\SQL2017_KeyAsm\bin\Debug\SQL2017_KeyAsm.dll"

# Get a code signing certificate
$cert = Get-ChildItem -Path "Cert:\CurrentUser\My" -CodeSigningCert
if ($cert -eq $null) {
    $cert = New-SelfSignedCertificate -CertStoreLocation cert:\currentuser\my -DnsName SQLtest.com -Type CodeSigningCert
}
# Create a password
$pwd = ConvertTo-SecureString -String 'ThisIsATestCert1' -Force -AsPlainText
# Specify where the certificate is located
$path = 'cert:\currentuser\my\' + $cert.thumbprint
# Export the certificate
Export-PfxCertificate -cert $path -FilePath .\SQLtest.pfx -Password $pwd
# Sign the empty assembly
Set-AuthenticodeSignature -FilePath $Assembly -Certificate $cert
# Sign the SimpleCrypto assembly
Set-AuthenticodeSignature -FilePath .\SQLCryptExt\bin\Debug\SimpleCrypto.dll -Certificate $cert
Write-Host "converting objects"
# Convert empty assembly to bytes
& .\BinaryFormatter.exe $Assembly .\SQL2017-KeyAsm.sql 40
# Convert cert to bytes
$certFile = Export-Certificate -Cert $cert -FilePath .\SQL2017-ClrStrictSecurity-Cert.cer
& .\BinaryFormatter.exe $certFile .\SQL2017-ClrStrictSecurity-Cert.sql 40

# Convert SQLCryptExt dll to bytes
& .\BinaryFormatter.exe .\SQLCryptExt\bin\Debug\SQLCryptExt.dll .\SQLCryptExt.sql 40
& .\BinaryFormatter.exe .\SQLCryptExt\bin\Debug\SimpleCrypto.dll .\SimpleCrypto.sql 40
